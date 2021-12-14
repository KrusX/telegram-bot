using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace InrecoTelegram.Bot.Command
{
    public abstract class Command
    {
        public abstract string [] Names { get; set; }
        public abstract void Execute(Message message, ITelegramBotClient client, CancellationToken cancellationToken);

        public bool Equals(string message)
        {
            foreach(var mess in Names)
            {
                if (message == mess)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
