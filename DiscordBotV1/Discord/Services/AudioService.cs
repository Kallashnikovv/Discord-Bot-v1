using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using Victoria;
using Victoria.Entities;

namespace DiscordBotV1.Discord.Services
{
    public class AudioService
    {
        private LavaRestClient _lavaRestClient;
        private LavaSocketClient _lavaSocketClient;
        private DiscordSocketClient _client;
        private DiscordLogger _logger;
        private LavaPlayer _player;

        private SocketVoiceChannel _voiceChannel;

        public AudioService(LavaRestClient lavaRestClient, LavaSocketClient lavaSocketClient, DiscordSocketClient client, DiscordLogger logger)
        {
            _logger = logger;
            _client = client;
            _lavaRestClient = lavaRestClient;
            _lavaSocketClient = lavaSocketClient;
        }

        public Task InitializeAsync()
        {
            HookEvents();
            return Task.CompletedTask;
        }
        private void HookEvents()
        {
            _client.Ready += ClientReadyAsync;
            _lavaSocketClient.Log += _logger.Log;
            _lavaSocketClient.OnTrackFinished += TrackFinished;
        }

        private async Task ClientReadyAsync()
        {
            await _lavaSocketClient.StartAsync(_client, new Configuration
            {
                LogSeverity = LogSeverity.Info
            });
        }

        private async Task TrackFinished(LavaPlayer player, LavaTrack track, TrackEndReason reason)
        {
            if (reason.ShouldPlayNext()) return;

            if (player.Queue.TryDequeue(out var item) || !(item is LavaTrack nextTrack))
            {
                await player.TextChannel.SendMessageAsync("There are no more tracks in the queue.");
                return;
            }

            await player.TextChannel.SendMessageAsync($"Now playing: {nextTrack}");
            await player.PlayAsync(nextTrack);
        }
     
        #region Methods
        public async Task ConnectAsync(SocketVoiceChannel voiceChannel, ITextChannel textChannel)
        {
            _voiceChannel = voiceChannel;
            await _lavaSocketClient.ConnectAsync(_voiceChannel, textChannel);
        }

        public async Task DisconnectAsync()
        {
            if (_voiceChannel is null) return;
            await _lavaSocketClient.DisconnectAsync(_voiceChannel);
            _voiceChannel = null;
        }

        public async Task<string> PlayAsync(string query, ulong guildId, SocketVoiceChannel voiceChannel, ITextChannel textChannel)
        {
            if(_voiceChannel is null)
            {
                _voiceChannel = voiceChannel;
                await ConnectAsync(_voiceChannel, textChannel);
            }
            _player = _lavaSocketClient.GetPlayer(guildId);
            var results = await _lavaRestClient.SearchYouTubeAsync(query);

            if (results.LoadType == LoadType.NoMatches || results.LoadType == LoadType.LoadFailed)
            {
                return "No matches found.";
            }

            var track = results.Tracks.FirstOrDefault();

            if (_player.IsPlaying)
            {
                _player.Queue.Enqueue(track);
                return $"{track.Title} has been added to the queue!";
            }
            else
            {
                await _player.PlayAsync(track);
                return $"Now playing: {track.Title}";
            }
        }

        public async Task StopAsync()
        {
            if(_player is null) return;

            await _player.StopAsync();
        }

        public async Task<string> SkipAsync()
        {
            if(_player is null || _player.Queue.Count is 0) return "Nothing in queue.";

            var oldTrack = _player.CurrentTrack;
            await _player.SkipAsync();
            return $"Skipped: {oldTrack.Title} \nNow playing: {_player.CurrentTrack.Title}";
        }

        public async Task<string> SetVolumeAsync(int vol)
        {
            if (_player is null) return "Player isn't playing.";

            if (vol > 1000 || vol < 1) return "Please use a number between 1 - 1000";
            await _player.SetVolumeAsync(vol);
            return $"Volume set to : {vol}%";
        }

        public async Task<string> PauseOrResumeAsync()
        {
            if (_player is null) return "Player isn't playing.";

            if (!_player.IsPaused)
            {
                await _player.PauseAsync();
                return "Player is paused.";
            }
            else
            {
                await _player.ResumeAsync();
                return "Playback resumed.";
            }
        }
        public async Task<string> ResumeAsync()
        {
            if (_player is null) return "Player isn't playing.";

            if (_player.IsPaused)
            {
                await _player.ResumeAsync();
                return "Playback resumed.";
            }

            return "Player is not paused.";
        }

        public string NowPlaying()
        {
            if (_player is null) return "Player isn't playing.";

            return $"{_player.CurrentTrack.Title}";
        }

        public async Task<string> SeekAsync(TimeSpan time)
        {
            if (_player is null) return "Player isn't playing.";

            await _player.SeekAsync(time);
            return $"Seeked {_player.CurrentTrack.Title} to {time}";
        }

        #endregion
        
    }
}
