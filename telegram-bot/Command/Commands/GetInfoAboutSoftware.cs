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
    class GetInfoAboutSoftware : Command
    {
        public override string[] Names { get; set; } = new string[] { "вопросы по по и оборудованию", "оборудование", "по и оборудование"};

        public override async void Execute(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "По вопросам по ПО и оборудованию обращаться к <a href=\"tg://user?id=1283115295\">Петрову А. А.</a>",
                parseMode: ParseMode.Html,
                replyToMessageId: message.MessageId,
                cancellationToken: cancellationToken);
        }
    }
}