using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBotV1.Discord.CommandModules
{
    public class DebugModule : ModuleBase<SocketCommandContext>
    {
        [Command("spam")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Spam(int amount = 5, [Remainder]string msg = "~Spam!")
        {
            int x = 0;
            while(amount != 0)
            {
                x++;
                amount--;
                await Context.Channel.SendMessageAsync(x + ". " + msg);
            }
        }
    }
}
