using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DiscordBotV1.Discord.CommandModules
{
    public class FunModule : ModuleBase<SocketCommandContext>
    {
        [Command("jpg")]
        private async Task SendRandImage()
        {
            string[] images;
            images = new String[]
            {
            "jpg/konon1.jpg",
            "jpg/konon2.jpg",
            "jpg/konon3.jpg",
            "jpg/konon4.jpg",
            "jpg/konon5.jpg",
            "jpg/konon6.jpg"
            };

            Random rand;
            rand = new Random();

            int randomIndex = rand.Next(images.Length);

            string imgToPost = images[randomIndex];

            await Context.Channel.SendFileAsync(imgToPost);
        }

        [Command("papaj")]
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
            DateTime hour = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 37, 00);
            DateTime now = DateTime.Now;
            DateTime a = new DateTime();
            if (now.Hour >= 21 && now.Minute > 37 && now.Second > 0 || now.Hour >= 22) { a = hour.AddDays(1); } else { a = hour; }
            TimeSpan diff1 = a.Subtract(now);
            await Context.Channel.SendMessageAsync($"Zostało jeszcze {diff1.Hours} godzin {diff1.Minutes} minut {diff1.Seconds} sekund.");
        }

        [Command("8ball")]
        public async Task YesNoGame([Remainder]string question)
        {

        }

    }
}
