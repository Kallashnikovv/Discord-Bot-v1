using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBot.Discord.CommandModules
{
    [Group, Name("Config")]
    public class ConfigModule : ModuleBase<SocketCommandContext>
    {
        [Command("CPrefix")]
        [RequireOwner]
        public async Task ChangePrefix(string prefix)
        {
            var oldPrefix = Global.BotConfig.Prefix;

            Global.BotConfig.Prefix = prefix;
            await ReplyAsync($"Prefix changed from {oldPrefix} to {prefix} for now!");
        }
    }
}
