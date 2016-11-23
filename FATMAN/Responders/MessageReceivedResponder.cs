using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using FATMAN.Managers;

namespace FATMAN.Responders
{
    public class MessageReceivedResponder : BaseResponder<SocketMessage>
    {
        public override async Task RespondAsync(SocketMessage eventInfo)
        {
            if (eventInfo.Content.StartsWith("!"))
            {
                var split = eventInfo.Content.Split();

                switch (split.Count())
                {
                    case 1:
                        await SingleWordCommandAsync(eventInfo);
                        break;

                    case 2:
                        await DoubleWordCommandAsync(eventInfo);
                        break;
                }
            }
            else
            {
                var discordUser = DiscordUserManager.Instance.GetDiscordUser(eventInfo.Author.Id);

                if (discordUser != null)
                {
                    discordUser.MessageCount++;
                }
            }
        }

        private async Task DoubleWordCommandAsync(SocketMessage eventInfo)
        {
            var split = eventInfo.Content.Split();
            var messageFragments = new List<string>();

            switch (split[0])
            {
                case "!login":

                    var discordUser = DiscordUserManager.Instance.DiscordUsers.SingleOrDefault(x => x.SocketUser.Username.ToUpper() == split[1].ToUpper());

                    if (discordUser != null)
                    {
                        if (discordUser.LastLogin != null)
                        {
                            messageFragments.Add("<@!" + discordUser.SocketUser.Id + "> last logged in " + ((DateTime)discordUser.LastLogin).ToString());
                        }
                        else
                        {
                            messageFragments.Add(":grey_question: <@!" + discordUser.SocketUser.Id + "> has never logged in :grey_question:");
                        }
                    }

                    break;

                case "!powerlevel":

                    var powerLevel = Globals.Random.Next(10000);

                    messageFragments.Add(":level_slider: <@!" + eventInfo.Author.Id + ">'s Power Level is " + powerLevel + " :level_slider:");

                    if (powerLevel > 9000)
                    {
                        messageFragments.Add("http://i.imgur.com/3sGoNdr.gif");
                    }

                    break;

                case "!camels":

                    var camelCount = Globals.Random.Next(100);

                    messageFragments.Add(":dromedary_camel: <@!" + eventInfo.Author.Id + "> is the proud owner of " + camelCount + " camels :dromedary_camel:");

                    break;
            }

            await DiscordManager.Instance.SpeakTextChannel.SendMessageAsync(string.Join(Environment.NewLine, messageFragments));
        }

        private async Task SingleWordCommandAsync(SocketMessage eventInfo)
        {
            switch (eventInfo.Content)
            {
                case "!uptime":
                    await DiscordManager.Instance.SpeakTextChannel.SendMessageAsync(":clock10: The FATMAN has been awake since " + DiscordManager.Instance.StartUpTime.ToString() + " :clock10:");
                    break;

                case "!logins":

                    var usersWithLogins = DiscordUserManager.Instance.DiscordUsers.Where(x => x.LastLogin != null).OrderBy(x => x.LastLogin);

                    var messageLines = new List<string>();

                    foreach (var userWithLogin in usersWithLogins)
                    {
                        messageLines.Add("• <@!" + userWithLogin.SocketUser.Id + "> last logged in " + ((DateTime)userWithLogin.LastLogin).ToString());
                    }

                    if (messageLines.Any())
                        await DiscordManager.Instance.SpeakTextChannel.SendMessageAsync(string.Join(Environment.NewLine, messageLines));

                    break;
            }
        }
    }
}
