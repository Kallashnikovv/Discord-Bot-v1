using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

[Group, Name("Reactions")]
public class ReactionModule : ModuleBase<SocketCommandContext>
{
    [Command("React")]
    public async Task ReactTo(ulong id, string emote = null)
    {
        var message = Context.Channel.GetCachedMessage(id);
        var msg = message as SocketUserMessage;

        if (emote is null)
        {
        var emoji = new Emoji("😂");
        await msg.AddReactionAsync(emoji);
        }
        else
        {
        var emoji = new Emoji(emote);
        await msg.AddReactionAsync(emoji);
        }
    }
}