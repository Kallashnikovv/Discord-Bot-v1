using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBot.Discord.CommandModules
{
    public class ConfigModule : ModuleBase<SocketCommandContext>
    {
        [Command("cprefix")]
        [RequireOwner]
        public async Task ChangePrefix(string prefix)
        {
            await Context.Channel.SendMessageAsync("IT WORKS!");
        }


    }
}
