using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordBotV1.Discord
{
	public class DiscordLogger
	{
        private readonly ILogger _logger;

		public DiscordLogger(ILogger logger)
		{
			_logger = logger;
		}

		public Task Log(LogMessage logMsg)
		{
			_logger.Log(logMsg.Message);
			
			return Task.CompletedTask;
		}
	}
}
