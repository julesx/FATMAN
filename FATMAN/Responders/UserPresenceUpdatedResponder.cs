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
            var messageFragments = new List<string>();

            if (arg3.Status == UserStatus.Offline && (arg4.Status == UserStatus.AFK || arg4.Status == UserStatus.Online || arg4.Status == UserStatus.Idle))
            {
                var discordUser = DiscordUserManager.Instance.GetDiscordUser(arg2.Id);

                if (discordUser.LastLogin != null && discordUser.LastLogin >= DateTime.Now.AddHours(-4))
                {
                    messageFragments.Add(":trumpet: **<@!" + arg2.Id + ">** has joined us.  Herald his arrival! :trumpet:");
                }

                discordUser.UpdateLastLogin();
            }
            else if (arg4.Game.HasValue && ((arg3.Game.HasValue && arg3.Game.Value.Name != arg4.Game.Value.Name) || !arg3.Game.HasValue))
            {
                switch (arg4.Game.Value.Name)
                {
                    case "Wargame Red Dragon":
                        messageFragments.Add(":crossed_swords: **<@!" + arg2.Id + ">** has engaged on the battlefield of the red dragon :crossed_swords:");
                        break;

                    case "Overwatch":
                        messageFragments.Add(":crossed_swords: **<@!" + arg2.Id + ">** is now under sighting :crossed_swords:");
                        break;

                    case "Diablo 3":
                        messageFragments.Add(":crossed_swords: **<@!" + arg2.Id + ">** would like to stay a while and listen :crossed_swords:");
                        break;

                    case "Starcraft 2":
                        messageFragments.Add(":crossed_swords: **<@!" + arg2.Id + ">** just brought his battlecruiser online :crossed_swords:");
                        break;

                    default:
                        messageFragments.Add(":crossed_swords: **<@!" + arg2.Id + ">** has entered the field of battle :crossed_swords:");
                        break;
                }

            }

            await DiscordManager.Instance.SpeakTextChannel.SendMessageAsync(string.Join(Environment.NewLine, messageFragments));
        }
    }
}
