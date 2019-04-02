using Discord;
using Discord.WebSocket;

namespace DiscordBotV1.Discord
{
	public class SocketConfig
	{
		public static DiscordSocketConfig GetDefault()
		{
			return new DiscordSocketConfig
			{
				LogLevel = LogSeverity.Verbose
			};
		}

		public static DiscordSocketConfig GetNew()
		{
			return new DiscordSocketConfig();
		}
	}
}
