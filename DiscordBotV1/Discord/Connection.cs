using DiscordBotV1.Discord.Entities;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordBotV1.Discord
{
	public class Connection
	{
		private DiscordSocketClient _client;
		private DiscordLogger _logger;

		public Connection(DiscordLogger logger)
		{
			_logger = logger;
		}

		internal async Task ConnectAsync(BotConfig config)
		{
			_client = new DiscordSocketClient(config.SocketConfig);

			_client.Log += _logger.Log;


		}
	}
}
