using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System;
using DiscordBot.Discord.Services;
using Discord.WebSocket;

public class AudioModule : ModuleBase<SocketCommandContext>
{
    private readonly AudioService _audioService;

    public AudioModule(AudioService audioService)
    {
        _audioService = audioService;
    }

    [Command("Join"), Alias("j")] 
    public async Task Join()
    {
        var user = Context.User as SocketGuildUser;
        if (user.VoiceChannel is null)
        {
            await ReplyAsync("You need to connect to a voice channel.");
            return;
        }
        else
        {
            await _audioService.ConnectAsync(user.VoiceChannel, Context.Channel as ITextChannel);
            await ReplyAsync($"Connected to {user.VoiceChannel.Name}!");
        }
    }
    
    [Command("Leave"), Alias("l")]
    public async Task Leave()
    {
        var user = Context.User as SocketGuildUser;
        await _audioService.DisconnectAsync();
        await ReplyAsync($"Disconnected from {user.VoiceChannel.Name}!");
    }

    [Command("Play"), Alias("p")]
    public async Task Play([Remainder]string query)
    {
        var user = Context.User as SocketGuildUser;
        await ReplyAsync(await _audioService.PlayAsync(query, Context.Guild.Id, user.VoiceChannel, Context.Channel as ITextChannel));
    }

    [Command("Search")]
    [RequireOwner]
    public async Task Search([Remainder]string query)
    {
        var user = Context.User as SocketGuildUser;
        await ReplyAsync(await _audioService.SearchAsync(query, Context.Guild.Id, user.VoiceChannel, Context.Channel as ITextChannel));
    }

    [Command("Stop")]
    public async Task Stop()
        => await _audioService.StopAsync();

    [Command("Skip")]
    public async Task Skip()
        => await ReplyAsync(await _audioService.SkipAsync());

    [Command("Volume"), Alias("vol")]
    public async Task Volume(int vol = 0)
        => await ReplyAsync(await _audioService.SetVolumeAsync(vol));

    [Command("Pause"), Alias("p")]
    public async Task Pause()
        => await ReplyAsync(await _audioService.PauseOrResumeAsync());

    [Command("Resume"), Alias("res")]
    public async Task Resume()
        => await ReplyAsync(await _audioService.ResumeAsync());

    [Command("NowPlaying"), Alias("np")]
    public async Task NowPlaying()
        => await ReplyAsync("", false, await _audioService.NowPlayingAsync());

    [Command("Seek"), Alias("s")]
    public async Task Seek(string query)
        => await ReplyAsync(await _audioService.SeekAsync(query));

    [Command("Queue"), Alias("q")]
    public async Task Queue()
        => await ReplyAsync(_audioService.Queue());
}