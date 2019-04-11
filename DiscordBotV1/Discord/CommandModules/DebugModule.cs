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

        [Command("avatar")]
        public async Task GetAvatar(IUser user = null)
        {
            if(user is null)
            {
                var avatarUrl = Context.User.GetAvatarUrl();
                var embed = new EmbedBuilder()
                    .WithImageUrl(avatarUrl)
                    .WithAuthor($"{Context.Message.Author.Username} avatar");
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            } else
            {
                var avatarUrl = user.GetAvatarUrl();
                var embed = new EmbedBuilder()
                    .WithImageUrl(avatarUrl)
                    .WithAuthor($"{user.Username} avatar");
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }
    }
}
