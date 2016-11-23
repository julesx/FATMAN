using Discord;
using Discord.WebSocket;
using System.Collections.Generic;
using FATMAN.Enums;
using FATMAN.Responders;
using FATMAN.Interfaces;
using System;

namespace FATMAN.Managers
{
    public class DiscordManager
    {
        public static DiscordManager Instance { get; } = new DiscordManager();

        public DiscordSocketClient Client;
        public IGuild Guild;
        public ITextChannel SpeakTextChannel;

        private Dictionary<DiscordEvent, IResponder> _responders = new Dictionary<DiscordEvent, IResponder>();

        public DateTime StartUpTime;

        public DiscordManager()
        {
            Client = new DiscordSocketClient();
        }

        public async void InitAsync()
        {
            //_responders.Add(DiscordEvent.UserJoined, new UserUpdatedResponder());
            //_responders.Add(DiscordEvent.UserLeft, new UserUpdatedResponder());
            _responders.Add(DiscordEvent.MessageReceived, new MessageReceivedResponder());
            _responders.Add(DiscordEvent.UserUpdated, new UserUpdatedResponder());

            await Client.LoginAsync(TokenType.Bot, "MjUwMDg2NTk4MDgxOTA0NjQy.CxSulw.G1d-5uTOnysbbtTXpsfydh9Q5mY");
            await Client.ConnectAsync();

            StartUpTime = DateTime.Now;

            Guild = Client.GetGuild(91374358932496384);

            //Client.UserJoined += _responders[DiscordEvent.UserJoined].RespondAsync;
            Client.MessageReceived += _responders[DiscordEvent.MessageReceived].RespondAsync;
            //Client.UserLeft += _responders[DiscordEvent.UserLeft].RespondAsync;
            //Client.UserUpdated += _responders[DiscordEvent.UserUpdated].RespondAsync;
            Client.UserUpdated += Client_UserUpdated;
            Client.UserPresenceUpdated += new UserPresenceUpdatedResponder().RespondAsync;

            SpeakTextChannel = await Guild.GetTextChannelAsync(91374358932496384);

            //await SpeakTextChannel.SendMessageAsync(":robot: BEEP BOOP. THE FATMAN HAS ARRIVED. :robot:");
        }

        private System.Threading.Tasks.Task Client_UserPresenceUpdated(Optional<SocketGuild> arg1, SocketUser arg2, SocketPresence arg3, SocketPresence arg4)
        {
            throw new System.NotImplementedException();
        }

        private System.Threading.Tasks.Task Client_UserUpdated(SocketUser arg1, SocketUser arg2)
        {
            throw new System.NotImplementedException();
        }
    }
}
