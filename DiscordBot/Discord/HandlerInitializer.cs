using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Discord.Handlers;
using DiscordBot.Discord.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Victoria;

namespace DiscordBot.Discord
{
    public class HandlerInitializer
    {
        public DiscordSocketClient _client;
        private readonly DiscordLogger _logger;
        private IServiceProvider _services;
        private readonly CommandService _commandService;
        private LoggingHandler _loggingHandler;
        private CommandHandler _commandHandler;
        private ReactionHandler _reactionHandler;

        public HandlerInitializer(DiscordSocketClient client, CommandService commandService, DiscordLogger logger)
        {
            _client = client;
            _commandService = commandService;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            _services = SetupServices();

            _commandHandler = new CommandHandler(_client, _commandService, _services, _logger);
            await _commandHandler.InitializeAsync();

            _loggingHandler = new LoggingHandler(_client);
            await _loggingHandler.Initialize();

            _reactionHandler = new ReactionHandler(_client);
            await _reactionHandler.Initialize();

            await _services.GetRequiredService<AudioService>().InitializeAsync();
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
