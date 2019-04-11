using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System;

public class AudioModule : ModuleBase<SocketCommandContext>
{

    [Command("join", RunMode = RunMode.Async)] 
    public async Task JoinChannel(IVoiceChannel channel = null)
    {
        // Get the audio channel
        channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
        if (channel == null) { await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

        // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
        var AudioClient = await channel.ConnectAsync();
        
        Console.WriteLine($"{Context.Message.Author} {channel.Name}");
        await ReplyAsync($"Connected to {channel.Name}");
    }
    
    /*[Command("leave", RunMode = RunMode.Async)]
    public async Task LeaveChannel()
    {
        
    }

    [Command("play", RunMode = RunMode.Async)]
    public async Task PlayCmd([Remainder]string song)
    {
        
    }*/
}