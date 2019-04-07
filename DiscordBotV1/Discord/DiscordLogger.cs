using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBotV1.Discord
{
	public class DiscordLogger : ModuleBase<SocketCommandContext>
	{
		ILogger _logger;

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
