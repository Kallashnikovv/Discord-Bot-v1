using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

public class ReactionModule : ModuleBase<SocketCommandContext>
{
    [Command("T")]
    [RequireBotPermission(GuildPermission.ViewChannel)]
    [RequireBotPermission(GuildPermission.SendMessages)]
    public async Task Test()
    {
        await Context.Message.DeleteAsync();
        await Context.Channel.SendMessageAsync();
    }


}