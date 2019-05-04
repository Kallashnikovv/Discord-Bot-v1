using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;
using Discord.Commands;
using DiscordBot.Discord.Converters;
using DiscordBot.Core.Services.Logger;
using System.Threading;
using System;

namespace DiscordBot.Discord
{
	public class Connection
	{
		public DiscordSocketClient _client;
		private readonly ILogger _logger;
        private CommandService _commandService;
        private HandlerInitializer _handlerInitializer;

		public Connection(ILogger logger, DiscordSocketClient client, CommandService commandService)
		{
            _logger = logger;
            _client = client;
            _commandService = commandService;
        }

		internal async Task ConnectAsync(CancellationToken cancellationToken)
		{
            _client.Log += LogAsync;
            try
            {
                await _client.LoginAsync(TokenType.Bot, Global.BotConfig.Token);
			    await _client.StartAsync();

			    await _client.SetGameAsync(Global.BotConfig.NowPlaying, null, ActivityType.Playing);
			    await _client.SetStatusAsync(UserStatus.DoNotDisturb);

                _handlerInitializer = new HandlerInitializer(_client, _commandService, _logger);
                await _handlerInitializer.InitializeAsync();

			    await Task.Delay(-1, cancellationToken);
            }
            catch (Exception)
            {
                await _client.StopAsync();
            }
		}

        private async Task LogAsync(LogMessage logMessage)
        {
            var DiscordBotLog = DiscordBotEntityConverter.CovertLog(logMessage);
            await _logger.LogAsync(DiscordBotLog);
        }
    }
}
