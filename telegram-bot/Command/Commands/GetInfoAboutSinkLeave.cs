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
    class GetInfoAboutSinkLeave : Command
    {
        public override string[] Names { get; set; } = new string[] { "больничный" };

        public override async void Execute(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Больничный с 15 по 30",
                cancellationToken: cancellationToken);
        }
    }
}
