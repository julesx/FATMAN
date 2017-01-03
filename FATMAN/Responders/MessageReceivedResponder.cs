using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using FATMAN.Managers;
using System.Net.Http;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;

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

            var commandTarget = (await DiscordManager.Instance.Guild.GetUsersAsync()).SingleOrDefault(x => x.Username.ToUpper() == split[1].ToUpper());

            switch (split[0])
            {
                case "!random":

                    switch (split[1])
                    {
                        case "cheese":
                            messageFragments.AddRange(await GetCheese());
                            break;
                    }

                    break;

                case "!whos":

                    switch (split[1])
                    {
                        case "hurr":
                            var users = await DiscordManager.Instance.Guild.GetUsersAsync();
                            var onlineUsers = users.Where(x => x.Status != Discord.UserStatus.Offline && x.Status != Discord.UserStatus.Unknown && !x.Username.Contains("FATMAN"));
                            messageFragments.Add(onlineUsers.Count() + " peeps are hurr");
                            break;
                    }

                    break;

                case "!download":

                    switch (split[1])
                    {
                        case "pdf":
                            messageFragments.Add("PDF DOWNLOAD INITIATED");
                            break;
                    }

                    break;

                //case "!login":

                //    if (commandTarget != null)
                //    {
                //        if (discordUser.LastLogin != null)
                //        {
                //            messageFragments.Add("<@!" + discordUser.SocketUser.Id + "> last logged in " + ((DateTime)discordUser.LastLogin).ToString());
                //        }
                //        else
                //        {
                //            messageFragments.Add(":grey_question: <@!" + discordUser.SocketUser.Id + "> has never logged in :grey_question:");
                //        }
                //    }

                //    break;

                case "!powerlevel":

                    var powerLevel = Globals.Random.Next(10000);

                    messageFragments.Add(":level_slider: <@!" + commandTarget.Id + ">'s Power Level is " + powerLevel + " :level_slider:");

                    if (powerLevel > 9000)
                    {
                        messageFragments.Add("http://i.imgur.com/3sGoNdr.gif");
                    }

                    break;

                case "!battle":
                    
                    if (commandTarget != null)
                    {
                        if (commandTarget.Id == eventInfo.Author.Id)
                        {
                            messageFragments.Add(":astonished: <@!" + commandTarget.Id + "> just beat himself up.  Nice. :astonished:");
                        }
                        else
                        {
                            var battleePowerLevel = Globals.Random.Next(10000);
                            var battlerPowerLevel = Globals.Random.Next(10000);

                            if (battleePowerLevel > battlerPowerLevel)
                            {
                                messageFragments.Add(":level_slider: <@!" + commandTarget.Id + "> (" + battleePowerLevel + ") just defeated <@!" + eventInfo.Author.Id + "> (" + battlerPowerLevel + ") in a huge upset :level_slider:");
                            }
                            else
                            {
                                messageFragments.Add(":level_slider: <@!" + eventInfo.Author.Id + "> (" + battlerPowerLevel + ") just defeated <@!" + commandTarget.Id + "> (" + battleePowerLevel + ") in a huge upset :level_slider:");
                            }
                        }
                    }

                    break;

                case "!camels":

                    var camelCount = Globals.Random.Next(100);

                    messageFragments.Add(":dromedary_camel: <@!" + commandTarget.Id + "> is the proud owner of " + camelCount + " dromedaries :dromedary_camel:");

                    break;
            }

            await DiscordManager.Instance.SpeakTextChannel.SendMessageAsync(string.Join(Environment.NewLine, messageFragments));
        }

        private async Task<List<string>> GetCheese()
        {
            var messageFragments = new List<string>();

            var xmlDeserializer = new XmlSerializer(typeof(XML_Structures.CheeseDirectory));

            using (var httpClient = new HttpClient())
            {
                var xml = await httpClient.GetStringAsync("http://cheese-fromage.agr.gc.ca/od-do/canadianCheeseDirectory.xml");

                using (TextReader reader = new StringReader(xml))
                {
                    var rootObject = (XML_Structures.CheeseDirectory)xmlDeserializer.Deserialize(reader);

                    var randomIndex = Globals.Random.Next(rootObject.Cheese.Count);

                    var randomCheese = rootObject.Cheese[randomIndex];

                    messageFragments.Add("**" + randomCheese.CheeseName + "**");

                    var messageFragment = "A *";

                    if (randomCheese.Characteristics != null)
                        messageFragment = messageFragment + randomCheese.Characteristics.Replace("Cheese", "").Replace("cheese", "") + " ";

                    messageFragment = messageFragment + randomCheese.CategoryType + "* cheese manufactured by *" + randomCheese.Manufacturer + "*.";

                    if (randomCheese.Flavour != null)
                        messageFragment = messageFragment + randomCheese.Flavour;

                    messageFragments.Add(messageFragment);
                }
            }

            return messageFragments;
        }

        private async Task<List<string>> GetFestival()
        {
            var messageFragments = new List<string>();

            using (var httpClient = new HttpClient())
            {
                var json = await httpClient.GetStringAsync("http://app.toronto.ca/cc_sr_v1_app/data/edc_eventcal_APR?limit=200");

                var calEvents = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Json_Structures.RootObject>>(json).Select(x => x.calEvent);

                calEvents = calEvents.OrderBy(x => DateTime.Parse(x.startDate));

                foreach (var calEvent in calEvents)
                {
                    var startDateTime = DateTime.Parse(calEvent.dates.First().startDateTime);
                    DateTime? endDateTime = null;

                    if (calEvent.dates.First().endDateTime != null)
                        endDateTime = DateTime.Parse(calEvent.dates.First().endDateTime);

                    if (startDateTime < DateTime.Now)
                        continue;

                    var location = calEvent.locations.First();

                    var whenDisplay = startDateTime.ToString("d") + " from " + startDateTime.ToString("t");

                    if (endDateTime != null)
                        whenDisplay = whenDisplay + " to " + DateTime.Parse(calEvent.dates.First().endDateTime).ToString("t");

                    messageFragments.Add("**WHAT**: " + calEvent.description.Substring(0, 100) + "...");
                    messageFragments.Add("**WHERE**: " + location.locationName + " (" + location.address + ")");
                    messageFragments.Add("**WHEN**: " + whenDisplay);
                    messageFragments.Add("http://app.toronto.ca" + calEvent.image.url);

                    break;
                }
            }

            return messageFragments;
        }

        private async Task SingleWordCommandAsync(SocketMessage eventInfo)
        {
            var messageFragments = new List<string>();

            switch (eventInfo.Content)
            {
                case "!festival":
                    messageFragments.AddRange(await GetFestival());
                    break;

                case "!bunupaspliff":
                    messageFragments.Add("https://goo.gl/35iDgU");
                    break;

                case "!bagman":
                    messageFragments.Add("https://goo.gl/GKh9dN");
                    break;

                case "!uptime":
                    messageFragments.Add(":clock10: The FATMAN has been eating cake since " + DiscordManager.Instance.StartUpTime.ToString() + " :clock10:");
                    break;

                case "!logins":

                    var usersWithLogins = DiscordUserManager.Instance.DiscordUsers.Where(x => x.LastLogin != null).OrderBy(x => x.LastLogin);

                    foreach (var userWithLogin in usersWithLogins)
                    {
                        messageFragments.Add("• <@!" + userWithLogin.SocketUser.Id + "> last logged in " + ((DateTime)userWithLogin.LastLogin).ToString());
                    }

                    break;
            }

            if (messageFragments.Any())
                await DiscordManager.Instance.SpeakTextChannel.SendMessageAsync(string.Join(Environment.NewLine, messageFragments));
        }
    }
}
