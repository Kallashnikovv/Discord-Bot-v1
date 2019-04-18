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
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100,
				LogLevel = LogSeverity.Debug,
                DefaultRetryMode = RetryMode.AlwaysRetry
            };
		}

		public static DiscordSocketConfig GetNew()
		{
			return new DiscordSocketConfig();
		}
	}
}
