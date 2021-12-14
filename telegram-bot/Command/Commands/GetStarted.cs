using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InrecoTelegram.Bot.Command.Commands
{
    class GetStarted : Command
    {
        private readonly ReplyKeyboardMarkup keyboardMain = new (
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

        public override string[] Names { get; set; } = new string[] { "/start" };

        public override async void Execute(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Какую информацию Вы хотите получить?",
                replyMarkup: keyboardMain,
                cancellationToken: cancellationToken);
        }
    }
}
