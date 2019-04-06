using Discord;
using Discord.Commands;
using System.Threading.Tasks;

public class BasicModule : ModuleBase<SocketCommandContext>
{
    [Command("echo")]
    public async Task Echo([Remainder]string message)
    {
        var embed = new EmbedBuilder();
        embed.WithTitle("Konon kiedyś powiedział:");
        embed.WithDescription(message);
        embed.WithColor(new Color(0, 255, 0));

        await Context.Channel.SendMessageAsync("", false, embed.Build());
    }

    [Command("echotts")]
    [RequireUserPermission(GuildPermission.SendTTSMessages)]
    public async Task TTSEcho([Remainder]string message)
    {
        await Context.Channel.SendMessageAsync(message, true);
    }

    [Command("chlebek", false), Alias("chleb")]
    private async Task SayChlebek()
    {
        await Context.Channel.SendMessageAsync("Chlebek Boży");
    }
    [Command("kabab", false), Alias("kebab")]
    private async Task SayKabab()
    {
        await Context.Channel.SendMessageAsync("Kabab Boży");
    }
}