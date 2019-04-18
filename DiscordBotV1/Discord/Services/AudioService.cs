using Discord;
using Discord.WebSocket;
using System;
using System.Globalization;
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

        private IVoiceChannel _voiceChannel;

        private TimeSpan hour = TimeSpan.Parse("01:00:00");
        private TimeSpan min = TimeSpan.Parse("10:00");

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
            if (!reason.ShouldPlayNext()) return;

            if (!player.Queue.TryDequeue(out var item) || !(item is LavaTrack nextTrack))
            {
                await player.TextChannel.SendMessageAsync("There are no more tracks in the queue.");
                return;
            }

            await player.PlayAsync(nextTrack);
            await player.TextChannel.SendMessageAsync($"Now playing: **{nextTrack}**");
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
                await ConnectAsync(voiceChannel, textChannel);
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
                if (track.Length >= hour)
                {
                return $"**{track.Title} :: {track.Length.ToString(@"hh\:mm\:ss")}** has been added to the queue!";
                }
                else if (track.Length >= min)
                {
                return $"**{track.Title} :: {track.Length.ToString(@"mm\:ss")}** has been added to the queue!";
                }
                else
                {
                return $"**{track.Title} :: {track.Length.ToString(@"m\:ss")}** has been added to the queue!";
                }
            }
            else
            {
                await _player.PlayAsync(track);
                if (_player.CurrentTrack.Length >= hour)
                {
                return $"Now playing: **{track.Title} :: {track.Length.ToString(@"hh\:mm\:ss")}**";
                }
                else if (_player.CurrentTrack.Length >= min)
                {
                return $"Now playing: **{track.Title} :: {track.Length.ToString(@"mm\:ss")}**";
                }
                else
                {
                return $"Now playing: **{track.Title} :: {track.Length.ToString(@"m\:ss")}**";
                }
            }
        }

        public async Task StopAsync()
        {
            if(_player is null) return;

            await _player.StopAsync();
        }

        public async Task<string> SkipAsync()
        {
            if(_player is null || _player.Queue.Items.Count() is 0) return "Nothing in queue.";

            var skipped = await _player.SkipAsync();
            if (_player.CurrentTrack.Length >= hour)
            {
                return $"Skipped: **{skipped.Title}** \nNow playing: **{_player.CurrentTrack.Title} :: {_player.CurrentTrack.Length.ToString(@"hh\:mm\:ss")}**";
            }
            else if (_player.CurrentTrack.Length >= min)
            {
                return $"Skipped: **{skipped.Title}** \nNow playing: **{_player.CurrentTrack.Title} :: {_player.CurrentTrack.Length.ToString(@"mm\:ss")}**";
            }
            else
            {
                return $"Skipped: **{skipped.Title}** \nNow playing: **{_player.CurrentTrack.Title} :: {_player.CurrentTrack.Length.ToString(@"m\:ss")}**";
            }
        }

        public async Task<string> SetVolumeAsync(int vol)
        {
            if (_player is null) return "Player isn't playing.";

            if (vol > 1000 || vol < 1) return "Please use a number between **1 - 1000**";
            await _player.SetVolumeAsync(vol);
            return $"Volume set to : **{vol}%**";
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

        public async Task<Embed> NowPlayingAsync()
        {
            if (_player is null) return default;

            var track = _player.CurrentTrack;
            var thumb = await track.FetchThumbnailAsync();
            TimeSpan time = TimeSpan.Parse(track.Position.ToString());
            var embed = new EmbedBuilder()
                .WithAuthor($"Now Playing: {_player.CurrentTrack.Title}", thumb, $"{track.Uri}")
                .WithThumbnailUrl(thumb)
                .AddField("Author", track.Author, true);

            if (track.Length >= hour)
            {
                embed.AddField("Length", $"{time.ToString(@"hh\:mm\:ss")}/{track.Length.ToString(@"hh\:mm\:ss")}", true);
            }
            else if (track.Length >= min)
            {
                embed.AddField("Length", $"{time.ToString(@"mm\:ss")}/{track.Length.ToString(@"mm\:ss")}", true);
            }
            else
            {
                embed.AddField("Length", $"{time.ToString(@"m\:ss")}/{track.Length.ToString(@"m\:ss")}", true);
            }

            return embed.Build();
        }

        public string Queue()
        {
            var tracks = _player.Queue.Items.Cast<LavaTrack>().Select(x => x.Title);
            return tracks.Count() is 0 ?
                "No tracks in queue." : string.Join("\n", tracks);
        }

        public async Task<string> SeekAsync(string value)
        {
            if (_player is null) return "Player isn't playing.";

            if (_player.CurrentTrack.Length >= hour)
            {
                TimeSpan time = TimeSpan.ParseExact(value, @"hh\:mm\:ss", CultureInfo.InvariantCulture);
                await _player.SeekAsync(time);
                return $"Seeked **{_player.CurrentTrack.Title}** to **{time.ToString(@"hh\:mm\:ss")}/{_player.CurrentTrack.Length.ToString(@"hh\:mm\:ss")}**";
            }
            else if (_player.CurrentTrack.Length >= min)
            {
                TimeSpan time = TimeSpan.ParseExact(value, @"mm\:ss", CultureInfo.InvariantCulture);
                await _player.SeekAsync(time);
                return $"Seeked **{_player.CurrentTrack.Title}** to **{time.ToString(@"mm\:ss")}/{_player.CurrentTrack.Length.ToString(@"mm\:ss")}**";
            }
            else
            {
                TimeSpan time = TimeSpan.ParseExact(value, @"m\:ss", CultureInfo.InvariantCulture);
                await _player.SeekAsync(time);
                return $"Seeked **{_player.CurrentTrack.Title}** to **{time.ToString(@"m\:ss")}/{_player.CurrentTrack.Length.ToString(@"m\:ss")}**";
            }
        }

        #endregion
        
    }
}
