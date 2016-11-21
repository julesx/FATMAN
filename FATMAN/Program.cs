using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using FATMAN.Managers;

namespace FATMAN
{
    class Program
    {
        static void Main(string[] args)
        {
            DiscordManager.Instance.InitAsync();
            Console.ReadLine();
        }
    }
}
