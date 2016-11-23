using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;

namespace FATMAN.Classes
{
    public class DiscordUser
    {
        public int MessageCount { get; set; }
        public DateTime? LastLogin { get; private set; }
        public IGuildUser SocketUser { get; set; }

        public List<string> EntranceGifs = new List<string>();

        public DiscordUser(IGuildUser socketUser, params string[] entranceGifs)
        {
            SocketUser = socketUser;

            foreach (var entranceGif in entranceGifs)
                EntranceGifs.Add(entranceGif);
        }

        public void UpdateMessageCount()
        {

        }

        public void UpdateLastLogin()
        {
            LastLogin = DateTime.Now;
        }
    }
}
