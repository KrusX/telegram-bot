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
    class GetInfoAboutVacation : Command
    {
        public override string[] Names { get; set; } = new string[] { "отпуск" };

        public override async void Execute(Message message, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Отпуск с 1 по 15",
                cancellationToken: cancellationToken);
        }
    }
}
