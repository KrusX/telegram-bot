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

var botClient = new TelegramBotClient("5081793184:AAEgWcQZo1LXLfeTenlqSSqAFvxvg24ShA8");

using var cts = new CancellationTokenSource();

var keyboardMain = new ReplyKeyboardMarkup
(
    new[]
    {
                        new[]
                        {
                            new KeyboardButton("Отпуск"),
                            new KeyboardButton("Больничный")
                        },
                        new[]
                        {
                            new KeyboardButton("Вопросы по ПО и оборудованию"),
                            new KeyboardButton("Программа компенсаций")
                        },
                        new[]
                        {
                            new KeyboardButton("Инфраструктура офиса")
                        }
    }
);

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

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Type != UpdateType.Message)
        return;

    if (update.Message!.Type != MessageType.Text)
        return;

    var chatId = update.Message.Chat.Id;
    var messageText = update.Message.Text;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "hi, hi, hi",
        replyMarkup: keyboardMain,
        cancellationToken: cancellationToken);
}

Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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