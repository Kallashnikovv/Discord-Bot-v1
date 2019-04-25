using Discord.Commands;
using DiscordBot.Storage;
using System.Threading.Tasks;

namespace DiscordBot.Discord.CommandModules
{
    public class ConfigModule : ModuleBase<SocketCommandContext>
    {
        [Command("cprefix")]
        [RequireOwner]
        public async Task ChangePrefix(string prefix)
        {
            var oldPrefix = Global.BotConfig.Prefix;

            Global.BotConfig.Prefix = prefix;
            await ReplyAsync($"Prefix changed from {oldPrefix} to {prefix} for now!");
        }
    }
}
