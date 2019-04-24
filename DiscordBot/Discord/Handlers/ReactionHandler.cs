using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DiscordBot.Discord.Handlers
{

    public class ReactionHandler
    {
        private readonly DiscordSocketClient _client;

        public ReactionHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        public Task Initialize()
        {
            _client.ReactionAdded += OnReactionAdded;

            return Task.CompletedTask;
        }

        private async Task OnReactionAdded(Cacheable<IUserMessage, ulong> msg, ISocketMessageChannel channel, SocketReaction reaction)
        {
            
            

        }
    }
}
