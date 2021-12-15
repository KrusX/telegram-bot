using InrecoTelegram.Bot.Command.Commands;
using InrecoTelegram.Bot.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace InrecoTelegram.Bot
{
    class Program
    {
        private static readonly string _connString = RepositoryBase.GetConnectionString();
        private static List<Command.Command> _commands;

        private static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            var botClient = new TelegramBotClient(Environment.GetEnvironmentVariable("TOKEN"));
            using var cts = new CancellationTokenSource();
            
            _commands = new()
            {
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

        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
                return;

            if (update.Message!.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text.ToLower();
            
            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            if (await IsUserValid(chatId, cancellationToken))
            {
                foreach (var command in _commands)
                {
                    if (command.Equals(messageText))
                    {
                        command.Execute(update.Message, botClient, cancellationToken);
                    }
                }
            }
        }

        private static async Task<bool> IsUserValid(long chatId, CancellationToken cancellationToken)
        {
            await using var conn = new NpgsqlConnection(_connString);
            await conn.OpenAsync(cancellationToken);

            await using var cmd = new NpgsqlCommand("SELECT id FROM users", conn);
            await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

            while (await reader.ReadAsync(cancellationToken))
            {
                if (reader.GetInt64(0) == chatId)
                {
                    return true;
                }
            }
            return false;
        }

        private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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