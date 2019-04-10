using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

public class BasicModule : ModuleBase<SocketCommandContext>
{

    [Command("say")]
    [RequireBotPermission(GuildPermission.ViewChannel)]
    [RequireBotPermission(GuildPermission.SendMessages)]
    public async Task Say([Remainder]string message)
    {
        await Context.Message.DeleteAsync();
        await Context.Channel.SendMessageAsync(message);
    }

    [Command("emoji")]
    public async Task Emoji(string emojiName)
    {
        var emoji = Context.Guild.Emotes.FirstOrDefault(x => x.Name == emojiName);
        if (emoji != null) { await ReplyAsync($"{emoji}"); await Context.Message.DeleteAsync(); }
        else { await ReplyAsync("Emoji not found."); }
    }

    [Command("echo")]
    [RequireBotPermission(GuildPermission.ViewChannel)]
    [RequireBotPermission(GuildPermission.SendMessages)]
    public async Task Echo([Remainder]string message)
    {
        var embed = new EmbedBuilder();
        embed.WithTitle("Echoed");
        embed.WithDescription(message);
        embed.WithColor(new Color(0, 255, 0));

        await Context.Channel.SendMessageAsync("", false, embed.Build());
    }

    [Command("echotts")]
    [RequireUserPermission(GuildPermission.SendTTSMessages)]
    [RequireBotPermission(GuildPermission.ViewChannel)]
    [RequireBotPermission(GuildPermission.SendMessages)]
    public async Task TTSEcho([Remainder]string message)
    {
        await Context.Channel.SendMessageAsync(message, true);
    }

    [Command("test", true), Alias("attempt")]
    [RequireBotPermission(GuildPermission.ViewChannel)]
    [RequireBotPermission(GuildPermission.SendMessages)]
    private async Task SayTest()
    {
        await Context.Channel.SendMessageAsync("1, 2, 3, test, test, test.");
    }

    [Command("chlebek", true), Alias("chleb")]
    [RequireBotPermission(GuildPermission.ViewChannel)]
    [RequireBotPermission(GuildPermission.SendMessages)]
    private async Task SayChlebek()
    {
        await Context.Channel.SendMessageAsync("Chlebek Boży");
    }

    [Command("kabab", true), Alias("kebab")]
    [RequireBotPermission(GuildPermission.ViewChannel)]
    [RequireBotPermission(GuildPermission.SendMessages)]
    private async Task SayKabab()
    {
        await Context.Channel.SendMessageAsync("Kabab Boży");
    }

    [Command("jpg")]
    private async Task SendRandImage()
    {
        string[] images;
        images = new String[]
        {
            "jpg/meme1.jpg",
            "jpg/meme2.jpg",
            "jpg/meme3.jpg",
        };

        Random rand;
        rand = new Random();

        int randomIndex = rand.Next(images.Length);

        string imgToPost = images[randomIndex];

        await Context.Channel.SendFileAsync(imgToPost);
    }
}

//Konon kiedyś powiedział:

//          "jpg/konon1.jpg",
//          "jpg/konon2.jpg",
//          "jpg/konon3.jpg",