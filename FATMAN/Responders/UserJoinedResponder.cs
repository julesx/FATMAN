using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using FATMAN.Managers;
using Discord;

namespace FATMAN.Responders
{
    public class UserJoinedResponder : BaseResponder
    {
        public Task Respond(SocketGuildUser socketGuildUser)
        {
            DiscordManager.Instance.SpeakTextChannel.SendMessageAsync(":trumpet: **" + socketGuildUser.Id + "** has joined us.  Herald his arrival! :trumpet:");
            return Task.CompletedTask;
        }
    }
}
