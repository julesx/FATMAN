using System.Collections.Generic;
using FATMAN.Classes;
using System.Linq;
using System.Threading.Tasks;

namespace FATMAN.Managers
{
    public class DiscordUserManager
    {
        public static DiscordUserManager Instance { get; } = new DiscordUserManager();

        public List<DiscordUser> DiscordUsers { get; } = new List<DiscordUser>();

        private Dictionary<ulong, string> _friends = new Dictionary<ulong, string>()
        {
            {90548197604212736, "http://i.imgur.com/biDFHU0.gif" },
            {226749613154107392, "http://www.reactiongifs.com/wp-content/uploads/2013/06/supa-hot-fire.gif" },
            {229357796691410944, "http://i.imgur.com/kIS2bKX.gif" },
            {171392216390959106, "https://media.giphy.com/media/26tjZlGxNsLeIXpfi/giphy.gif" },
            {230715453687726081, "https://media.giphy.com/media/ReltUjiLEcpdS/giphy.gif" },
            {228350284148113409, "https://media.giphy.com/media/12YpCtHZrngfa8/giphy.gif" }
        };

        public DiscordUserManager()
        {
            LoadFriendsAsync();
        }

        private async Task LoadFriendsAsync()
        {
            foreach (var friend in _friends)
            {
                var socketUser = await DiscordManager.Instance.Guild.GetUserAsync(friend.Key);
                DiscordUsers.Add(new DiscordUser(socketUser, friend.Value));
            }
        }

        public DiscordUser GetDiscordUser(ulong id)
        {
            return DiscordUsers.SingleOrDefault(x => x.SocketUser.Id == id);
        }
    }
}
