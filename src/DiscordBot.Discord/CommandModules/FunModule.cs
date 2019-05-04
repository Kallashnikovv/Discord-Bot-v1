using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Discord.CommandModules
{
    [Group, Name("Fun")]
    public class FunModule : ModuleBase<SocketCommandContext>
    {
        [Command("Jpg")]
        private async Task SendRandImage()
        {
            string[] images = new String[]
            {
            "jpg/konon1.jpg",
            "jpg/konon2.jpg",
            "jpg/konon3.jpg",
            "jpg/konon4.jpg",
            "jpg/konon5.jpg",
            "jpg/konon6.jpg"
            };

            Random rand = new Random();

            int randomIndex = rand.Next(images.Length);

            string imgToPost = images[randomIndex];

            await Context.Channel.SendFileAsync(imgToPost);
        }

        [Command("Papaj")]
        public async Task Papaj()
        {
            var embed = new EmbedBuilder()
                .WithAuthor("Jan Paweł II")
                .WithImageUrl("http://www.histurion.pl/grafika/slownik/xx_wiek/normal/jan_pawel_ii.jpg");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("2137")]
        public async Task TimeLeft()
        {
            DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 37, 00);
            DateTime now = DateTime.Now;
            DateTime a = new DateTime();

            if (now.Hour >= 21 && now.Minute > 37 && now.Second > 0 || now.Hour >= 22) { a = time.AddDays(1); } else { a = time; }
            TimeSpan diff1 = a.Subtract(now);

            var embed = new EmbedBuilder()
                .WithColor(255, 255, 255)
                .WithAuthor("Time left:");
            
            if (diff1.Hours >= 1)
            {
                embed.WithTitle($"{diff1.ToString(@"hh\:mm\:ss")}");
            }
            else if (diff1.Minutes >= 10)
            {
                embed.WithTitle($"{diff1.ToString(@"mm\:ss")}");
            }
            else
            {
                embed.WithTitle($"{diff1.ToString(@"m\:ss")}");
            }

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Chlebek", true), Alias("chleb")]
        [RequireBotPermission(GuildPermission.ViewChannel)]
        [RequireBotPermission(GuildPermission.SendMessages)]
        private async Task SayChlebek()
        {
            await Context.Channel.SendMessageAsync("Chlebek Boży");
        }

        [Command("Kabab", true), Alias("kebab")]
        [RequireBotPermission(GuildPermission.ViewChannel)]
        [RequireBotPermission(GuildPermission.SendMessages)]
        private async Task SayKabab()
        {
            await Context.Channel.SendMessageAsync("Kabab Boży");
        }

        [Command("8ball")]
        public async Task YesNoGame([Remainder]string question)
        {
            string[] answers;
            answers = new String[]
            {
                "Yes.",
                "No.",
                "Absolutly!",
                "(╯°□°）╯︵ ┻━┻"
            };
        }

    }
}
