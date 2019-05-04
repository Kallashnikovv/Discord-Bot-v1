using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

[Group, Name("Basic")]
public class BasicModule : ModuleBase<SocketCommandContext>
{
    [Command("Ping")]
    public async Task Ping()
    {
        await ReplyAsync($"Pong! {Context.Client.Latency}ms");
    }

    [Command("Say")]
    [RequireBotPermission(GuildPermission.ViewChannel)]
    [RequireBotPermission(GuildPermission.SendMessages)]
    public async Task Say([Remainder]string message)
    {
        await Context.Message.DeleteAsync();
        await Context.Channel.SendMessageAsync(message);
    }

    [Command("Emoji")]
    public async Task Emoji(string emojiName)
    {
        var emoji = Context.Guild.Emotes.FirstOrDefault(x => x.Name == emojiName);
        if (emoji != null) { await ReplyAsync($"{emoji}"); await Context.Message.DeleteAsync(); }
        else { await ReplyAsync("Emoji not found."); }
    }

    [Command("Echo")]
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

    [Command("EchoTTS")]
    [RequireUserPermission(GuildPermission.SendTTSMessages)]
    [RequireBotPermission(GuildPermission.ViewChannel)]
    [RequireBotPermission(GuildPermission.SendMessages)]
    public async Task TTSEcho([Remainder]string message)
    {
        await Context.Channel.SendMessageAsync(message, true);
    }
}   