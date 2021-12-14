using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace InrecoTelegram.Bot.Command.Commands
{
    class GetInfoAboutSoftware : Command
    {
        public override string[] Names { get; set; } = new string[] { "вопросы по по и оборудованию" };

        public override async void Execute(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "По вопросам по ПО и оборудованию обращаться к...",
                cancellationToken: cancellationToken);
        }
    }
}