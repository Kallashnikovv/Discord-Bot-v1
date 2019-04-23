using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

public class ReactionModule : ModuleBase<SocketCommandContext>
{
    [Command("T")]
    public async Task Test()
    {

    }
}