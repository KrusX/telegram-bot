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
    var messageText = update.Message.Text.ToLower();

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    await ChooseAnswerOption(botClient, chatId, messageText, cancellationToken);
}

async Task ChooseAnswerOption(ITelegramBotClient botClient, long chatId, string messageText, CancellationToken cancellationToken)
{
    if (messageText != null)
    switch (messageText)
    {
        case "/start":
            {
                await SendMessage(chatId, "О чем вы хотите получить информацию?", cancellationToken, keyboardMain);
                break;
            }
        case "отпуск":
            {
                await SendMessage(chatId, "Отпуск с 1 по 15", cancellationToken);
                break;
            }
        case "больничный":
            {
                await SendMessage(chatId, "Больничный с 15 по 30", cancellationToken);
                break;
            }
        case "вопросы по по и оборудованию":
            {
                await SendMessage(chatId, "Вопросы по ПО здесь ---", cancellationToken);
                break;
            }
        case "программа компенсаций":
            {
                await SendMessage(chatId, "Программа компенсаций включает в себя...", cancellationToken);
                break;
            }
        case "инфраструктура офиса":
            {
                await SendMessage(chatId, "Карта инфраструктуры офиса: 1, 2, 3 ....", cancellationToken);
                break;
            }
        default:
            {
                await SendMessage(chatId, "Выберите интересующий Вас вопрос.", cancellationToken);
                break;
            }
    }
}

async Task SendMessage(long chatId, string message, CancellationToken cancellationToken, ReplyKeyboardMarkup replyKeyboard = null)
{
    await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: message,
        replyMarkup: replyKeyboard,
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