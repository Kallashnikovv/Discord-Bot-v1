using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBotV1.Discord.CommandModules
{
    public class FunModule : ModuleBase<SocketCommandContext>
    {
        [Command("8ball")]
        public async Task YesNoGame([Remainder]string question)
        {

        }
    }
}
