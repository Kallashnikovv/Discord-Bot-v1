using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Discord.Handlers;
using DiscordBot.Discord.Services;
using DiscordBot.Core.Services.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Victoria;
using DiscordBot.Discord.CommandModules;

namespace DiscordBot.Discord
{
    public class HandlerInitializer
    {
        public DiscordSocketClient _client;
        private readonly ILogger _logger;
        private IServiceProvider _services;
        private readonly CommandService _commandService;
        private LoggingHandler _loggingHandler;
        private CommandHandler _commandHandler;
        private ReactionHandler _reactionHandler;
        private HelpModule _helpModule;

        public HandlerInitializer(DiscordSocketClient client, CommandService commandService, ILogger logger)
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

            _helpModule = new HelpModule(_commandService);

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
