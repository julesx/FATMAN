using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using FATMAN.Managers;
using Discord;

namespace FATMAN.Responders
{
    public class UserJoinedResponder : BaseResponder
    {
        public override Task Respond<TEventInfo>(TEventInfo eventInfo)
        {
            DiscordManager.Instance.SpeakTextChannel.SendMessageAsync(":trumpet: **" + socketGuildUser.Id + "** has joined us.  Herald his arrival! :trumpet:");
            return Task.CompletedTask;
        }
    }
}
