using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Collections.Generic;
using FATMAN.Enums;
using FATMAN.Responders;
using FATMAN.Interfaces;

namespace FATMAN.Managers
{
    public class DiscordManager
    {
        public static DiscordManager Instance { get; } = new DiscordManager();

        public DiscordSocketClient Client;
        public IGuild Guild;
        public ITextChannel SpeakTextChannel;

        private Dictionary<DiscordEvent, IResponder> _responders = new Dictionary<DiscordEvent, IResponder>();

        public DiscordManager()
        {
            Client = new DiscordSocketClient();
        }

        public async void InitAsync()
        {
            _responders.Add(DiscordEvent.UserJoined, new UserJoinedResponder());
            _responders.Add(DiscordEvent.UserLeft, new UserLeftResponder());
            _responders.Add(DiscordEvent.MessageReceived, new MessageReceivedResponder());

            await Client.LoginAsync(TokenType.Bot, "MYTOKEN");
            await Client.ConnectAsync();

            Guild = Client.GetGuild(91374358932496384);
            
            Client.UserJoined += _responders[DiscordEvent.UserJoined].Respond;
            Client.MessageReceived += _responders[DiscordEvent.MessageReceived].Respond;

            //Client.UserLeft += _responders[DiscordEvent.UserLeft].Respond;

            SpeakTextChannel = await Guild.GetTextChannelAsync(91374358932496384);

            //await SpeakTextChannel.SendMessageAsync(":robot: BEEP BOOP. THE FATMAN HAS ARRIVED. :robot:");
        }
    }
}
