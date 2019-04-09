using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBotV1.Discord.CommandModules
{
    public class DebugModule : ModuleBase<SocketCommandContext>
    {
        [Command("spam")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Spam(int amount = 10, [Remainder]string msg = "~Spam!")
        {
            while(amount != 0)
            {
                amount--;
                await Context.Channel.SendMessageAsync(msg);
            }
        }
    }
}
