using DiscordBotV1.Discord.Entities;
using DiscordBotV1.Discord.Handlers;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;

namespace DiscordBotV1.Discord
{
	public class Connection
	{
		public DiscordSocketClient _client;
		private readonly DiscordLogger _logger;
		private CommandHandler _handler;

		public Connection(DiscordLogger logger, DiscordSocketClient client)
		{
            _logger = logger;
            _client = client;
        }

		internal async Task ConnectAsync()
		{
			_client.Log += _logger.Log;

			await _client.LoginAsync(TokenType.Bot, Global.BotConfig.Token);
			await _client.StartAsync();
			await _client.SetGameAsync(Global.BotConfig.NowPlaying, null, ActivityType.Playing);
			await _client.SetStatusAsync(UserStatus.DoNotDisturb);
						
			_handler = new CommandHandler();
			await _handler.InitializeAsync(_client);

			await Task.Delay(-1);
		}
	}
}
