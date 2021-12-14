using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace InrecoTelegram.Bot.Command.Commands
{
    class GetInfoAboutCompensationProgram : Command
    {
        public override string[] Names { get; set; } = new string[] { "программа компенсаций", "компенсации" };

        public override async void Execute(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: @"С *программой компенсаций* можно ознакомиться на сайте\.",
                parseMode: ParseMode.MarkdownV2,
                replyToMessageId: message.MessageId,
                replyMarkup: new InlineKeyboardMarkup(
                    InlineKeyboardButton.WithUrl(
                        "Ознакомиться с программой компенсаций",
                        "https://github.com/KrusX")),
                cancellationToken: cancellationToken);;
        }
    }
}
