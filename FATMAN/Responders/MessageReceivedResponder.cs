using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace FATMAN.Responders
{
    public class MessageReceivedResponder : BaseResponder
    {
        public Task Respond(SocketMessage socketGuildUser)
        {
            return Task.CompletedTask;
        }
    }
}
