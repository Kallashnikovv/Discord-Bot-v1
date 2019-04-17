using DiscordBotV1.Discord.Handlers;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;
using Discord.Commands;
using System;
using Microsoft.Extensions.DependencyInjection;
using Victoria;
using DiscordBotV1.Discord.Services;

namespace DiscordBotV1.Discord
{
	public class Connection
	{
		public DiscordSocketClient _client;
		private readonly DiscordLogger _logger;
        private IServiceProvider _services;
        private readonly CommandService _commandService;
        private CommandHandler _handler;

		public Connection(DiscordLogger logger, DiscordSocketClient client, CommandService commandService)
		{
            _logger = logger;
            _client = client;
            _commandService = commandService;
        }

		internal async Task ConnectAsync()
		{
			_client.Log += _logger.Log;

			await _client.LoginAsync(TokenType.Bot, Global.BotConfig.Token);
			await _client.StartAsync();
			await _client.SetGameAsync(Global.BotConfig.NowPlaying, null, ActivityType.Playing);
			await _client.SetStatusAsync(UserStatus.DoNotDisturb);
            _services = SetupServices();

            _handler = new CommandHandler(_client, _commandService, _services, _logger);
            await _handler.InitializeAsync();

            await _services.GetRequiredService<AudioService>().InitializeAsync();

			await Task.Delay(-1);
		}

        private IServiceProvider SetupServices()
            => new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commandService)
            .AddSingleton(_logger)
            .AddSingleton<LavaRestClient>()
            .AddSingleton<LavaSocketClient>()
            .AddSingleton<AudioService>()
            .BuildServiceProvider();
	}
}
