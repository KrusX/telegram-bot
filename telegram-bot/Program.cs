using System;
using Telegram.Bot;

namespace InrecoTelegram.Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            var botClient = new TelegramBotClient("5081793184:AAEgWcQZo1LXLfeTenlqSSqAFvxvg24ShA8");
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
                $"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
        }
    }
}
