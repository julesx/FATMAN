using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;

namespace FATMAN.Responders
{
    public class UserLeftResponder : BaseResponder<SocketGuildUser>
    {
        public override Task RespondAsync(SocketGuildUser socketGuildUser)
        {
            return Task.CompletedTask;
        }
    }
}
