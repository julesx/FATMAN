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
            var discordUser = DiscordUserManager.Instance.GetDiscordUser(arg2.Id);

            if (discordUser == null)
                return;

            var messageFragments = new List<string>();

            if (arg3.Status == UserStatus.Offline && (arg4.Status == UserStatus.AFK || arg4.Status == UserStatus.Online || arg4.Status == UserStatus.Idle))
            {
                //if (discordUser.LastLogin != null && discordUser.LastLogin >= DateTime.Now.AddHours(-4))
                //{
                //    messageFragments.Add(":trumpet: **<@!" + arg2.Id + ">** has joined us.  Herald his arrival! :trumpet:");
                //}

                discordUser.UpdateLastLogin();
            }
            else if (arg4.Game.HasValue && ((arg3.Game.HasValue && arg3.Game.Value.Name != arg4.Game.Value.Name) || !arg3.Game.HasValue))
            {
                switch (arg4.Game.Value.Name)
                {
                    case "Wargame Red Dragon":
                        messageFragments.Add(":crossed_swords: **<@!" + arg2.Id + ">** is launching several PPK Fagots :crossed_swords:");
                        break;

                    case "Overwatch":
                        messageFragments.Add(":crossed_swords: **<@!" + arg2.Id + ">** is now ... SHPOURTEEEEING :crossed_swords:");
                        break;

                    case "Diablo 3":
                        messageFragments.Add(":crossed_swords: **<@!" + arg2.Id + ">** would like to stay a while and listen :crossed_swords:");
                        break;

                    case "Starcraft 2":
                        messageFragments.Add(":crossed_swords: **<@!" + arg2.Id + ">** is now firing it up :crossed_swords:");
                        break;

                    case "Counter-Strike: Global Offensive":
                        messageFragments.Add(":crossed_swords: **<@!" + arg2.Id + ">** has entered a virtual combat arena :crossed_swords:");
                        break;

                    case "Shadow Tactics":
                        messageFragments.Add(":crossed_swords: **<@!" + arg2.Id + ">** is a silent ninja :crossed_swords:");
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
