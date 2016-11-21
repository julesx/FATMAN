using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace FATMAN.Responders
{
    public class UserLeftResponder : BaseResponder
    {
        public override Task Respond<SocketGuildUser>(SocketGuildUser socketGuildUser)
        {
            return Task.CompletedTask;
        }
    }
}
