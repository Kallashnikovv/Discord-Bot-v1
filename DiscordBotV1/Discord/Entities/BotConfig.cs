using Discord.WebSocket;

namespace DiscordBotV1.Discord.Entities
{
	public class BotConfig
	{
		public string Token { get; set; }
		public string DefaultPrefix { get; set; }
		public string GameStatus { get; set; }
	}
}
