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

        private Dictionary<DiscordEvent, Action> _responders = new Dictionary<DiscordEvent, Action>();

        public DiscordManager()
        {
            Client = new DiscordSocketClient();
        }

        public async void InitAsync()
        {
            //_responders.Add(DiscordEvent.UserJoined, new UserJoinedResponder().Respond<SocketGuildUser>);
            //_responders.Add(DiscordEvent.UserLeft, new UserLeftResponder());

            await Client.LoginAsync(TokenType.Bot, "MjUwMDg2NTk4MDgxOTA0NjQy.CxSulw.G1d-5uTOnysbbtTXpsfydh9Q5mY");
            await Client.ConnectAsync();

            Guild = Client.GetGuild(91374358932496384);
            
            Client.UserJoined += _responders[DiscordEvent.UserJoined].Respond;
            Client.UserLeft += _responders[DiscordEvent.UserLeft].Respond;
            Client.MessageReceived += Client_MessageReceived;

            SpeakTextChannel = await Guild.GetTextChannelAsync(91374358932496384);

            await SpeakTextChannel.SendMessageAsync(":robot: BEEP BOOP. THE FATMAN HAS ARRIVED. :robot:");
        }

        private Task Client_MessageReceived(SocketMessage arg)
        {
            throw new NotImplementedException();
        }
    }
}
