using Discord.WebSocket;

namespace DiscordBotV1.Discord.Entities
{
	public class BotConfig
	{
		public string Token { get; set; }
		public string cmdPrefix { get; set; }
	}
}
