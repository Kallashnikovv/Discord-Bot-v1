using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

public class ServerManaging : ModuleBase<SocketCommandContext>
{
    
    [Command("cls")]
    [RequireUserPermission(GuildPermission.Administrator)]

    public async Task Cls(int amount = 5)
    {
        if (amount <= 100)
        {
            await Context.Channel.TriggerTypingAsync();

            var delay = 3;
            var msg = $"Bulk removing {amount} messages!";


            _ = SendAndDelayedDeleteMessageAsync(msg, delay, true);

            await Task.Delay(3000);
            amount += 1;
            var messages = await Context.Channel.GetMessagesAsync(amount).FlattenAsync();
            messages = messages.Where(m => !m.IsPinned);
            await ((ITextChannel) Context.Channel).DeleteMessagesAsync(messages);
        }
        else 
        {
            var errorMsg = "Can't remove that many messages at once!";
            var delay = 5;
            
            _ = SendAndDelayedDeleteMessageAsync(errorMsg, delay, true, true);
        }
    }

    [Command("Kick")]
    [RequireUserPermission(GuildPermission.KickMembers)]
    [RequireBotPermission(GuildPermission.KickMembers)]
    public async Task KickUser(IGuildUser user, [Remainder]string reason = "No reason provided.")
    {
        await user.KickAsync(reason);
        
        var embed = new EmbedBuilder();
        embed.WithAuthor(user);
        embed.WithTitle("Succesfully kicked, reason:");
        embed.WithDescription(reason);
        embed.WithColor(new Color(255, 155, 75));

        await Context.Channel.SendMessageAsync("", false, embed.Build());
    }

    [Command("Ban")]
    [RequireUserPermission(GuildPermission.BanMembers)]
    [RequireBotPermission(GuildPermission.BanMembers)]
    public async Task BanUser(IGuildUser user, int amount, [Remainder]string reason = "No reason provided.")
    {
        await user.Guild.AddBanAsync(user, amount, reason);

        var embed = new EmbedBuilder();
        embed.WithAuthor(user);
        embed.WithTitle("Succesfully banned, reason:");
        embed.WithDescription(reason);
        embed.WithColor(new Color(255, 0, 0));

        await Context.Channel.SendMessageAsync("", false, embed.Build());
    }

    #region Methods
    private async Task SendAndDelayedDeleteMessageAsync(string message, int delayInSeconds, bool delete = false, bool deleteCmnd = false)
    {
        var milisecoonds = delayInSeconds * 1000;
        var msg = await Context.Channel.SendMessageAsync(message);
        if(delete == true)
        {
        await Task.Delay(milisecoonds);
        await msg.DeleteAsync();
        }
        if(deleteCmnd == true) { await Context.Message.DeleteAsync(); }
    }
    #endregion
}