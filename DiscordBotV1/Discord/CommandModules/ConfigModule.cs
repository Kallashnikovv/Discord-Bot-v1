using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBotV1.Discord.CommandModules
{
    public class ConfigModule : ModuleBase<SocketCommandContext>
    {
        [Command("prefix")]
        public async Task ChangePrefix(string prefix)
        {

        }


    }
}
