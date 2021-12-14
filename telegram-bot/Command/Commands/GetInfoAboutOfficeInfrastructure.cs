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
    class GetInfoAboutOfficeInfrastructure : Command
    {
        public override string[] Names { get; set; } = new string[] { "инфраструктура офиса", "карта офиса" };

        public override async void Execute(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            await client.SendPhotoAsync(
                chatId: message.Chat.Id,
                photo: "https://res.cloudinary.com/krusx/image/upload/v1639487410/astrophysici_qkxzbv.jpg",
                caption: "1. Кабинет.\n2. Коридор.",
                replyToMessageId: message.MessageId,
                replyMarkup: new InlineKeyboardMarkup(
                    InlineKeyboardButton.WithUrl(
                        "Подробнее",
                        "https://github.com/KrusX")),
                cancellationToken: cancellationToken);
        }
    }
}
