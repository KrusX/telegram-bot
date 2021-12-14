using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace InrecoTelegram.Bot.Command.Commands
{
    class GetInfoAboutVacation : Command
    {
        public override string[] Names { get; set; } = new string[] { "отпуск" };

        public override async void Execute(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Задать вопрос по поводу отпуска можно <a href=\"tg://user?id=1283115295\">Петрову А. А.</a>",
                parseMode: ParseMode.Html,
                replyToMessageId: message.MessageId,
                cancellationToken: cancellationToken);
        }
    }
}
