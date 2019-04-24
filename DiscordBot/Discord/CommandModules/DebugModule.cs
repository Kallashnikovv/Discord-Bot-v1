using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DiscordBot.Discord.CommandModules
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

        [Command("Embed")]
        public async Task Embed()
        {
            var embed = new EmbedBuilder()
                .WithColor(Color.Red)
                .WithAuthor("Author")
                .WithTitle("Title")
                .WithDescription("Description")
                .WithThumbnailUrl("https://i.gyazo.com/05cf5976acd07ea1cd403bd307188337.gif")
                .WithImageUrl("https://i.gyazo.com/6be4f07c28af3693a98404f0fa1f9d80.jpg")
                .AddField("FieldName", "Object value")
                .AddField("FieldName2", "inline = true", true)
                .AddField("FieldName3", "inline = true", true)
                .WithFooter("Footer")
                .WithCurrentTimestamp()
                .Build();

            await ReplyAsync("Embed example:", false, embed);
        }

        [Command("DebugReact")]
        public async Task React()
        {
            var message = await Context.Channel.SendMessageAsync("Reaction test!");

            var emoji = new Emoji("😂");
            await message.AddReactionAsync(emoji);
        }
    }
}
