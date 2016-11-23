using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using FATMAN.Managers;
using Discord;

namespace FATMAN.Responders
{
    public class UserUpdatedResponder : BaseResponder<SocketGuildUser>
    {
        public override async Task RespondAsync(SocketGuildUser beforeInfo, SocketGuildUser afterInfo)
        {
           
        }
    }
}
