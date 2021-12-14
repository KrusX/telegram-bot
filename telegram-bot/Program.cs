
using InrecoTelegram.Bot.Command.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace InrecoTelegram.Bot
{
    class Program
    {
        private static TelegramBotClient botClient;
        private static List<Command.Command> commands;

        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            botClient = new TelegramBotClient(Config.Token);
            using var cts = new CancellationTokenSource();

            commands = new List<Command.Command>() {
                new GetStarted(),
                new GetInfoAboutVacation(),
                new GetInfoAboutSinkLeave(),
                new GetInfoAboutSoftware(),
                new GetInfoAboutCompensationProgram(),
                new GetInfoAboutOfficeInfrastructure()
            };

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            cts.Cancel();
        }

        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
                return;

            if (update.Message!.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text.ToLower();

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            foreach (var comm in commands)
            {
                if (comm.Equals(messageText))
                {
                    comm.Execute(update.Message, botClient, cancellationToken);
                }
            }
        }

        static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}