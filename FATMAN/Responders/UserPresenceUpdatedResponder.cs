using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using FATMAN.Managers;

namespace FATMAN.Responders
{
    public class UserPresenceUpdatedResponder
    {
        public async Task RespondAsync(Optional<SocketGuild> arg1, SocketUser arg2, SocketPresence arg3, SocketPresence arg4)
        {
            if (arg3.Status == UserStatus.Offline && (arg4.Status == UserStatus.AFK || arg4.Status == UserStatus.Online || arg4.Status == UserStatus.Idle))
            {
                var discordUser = DiscordUserManager.Instance.GetDiscordUser(arg2.Id);

                if (discordUser.LastLogin != null && discordUser.LastLogin >= DateTime.Now.AddHours(-4))
                {
                    var message = ":trumpet: **<@!" + arg2.Id + ">** has joined us.  Herald his arrival! :trumpet:";
                    await DiscordManager.Instance.SpeakTextChannel.SendMessageAsync(message);
                }

                discordUser.UpdateLastLogin();
            }
        }
    }
}
