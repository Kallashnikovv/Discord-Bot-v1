using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Discord;
using System;
using DiscordBot.Core.Services.Logger;
using DiscordBot.Core.Storage;

namespace DiscordBot.InversionOfControl
{
    public static class Container
    {
        public static IServiceCollection AddMiunieTypes(this IServiceCollection collection)
            => collection.AddSingleton<Random>()
                .AddSingleton<IDiscordBotClient, DiscordBotClient>()
                .AddSingleton<ILogger, DiscordBotLogger>()
                .AddSingleton<IPersistentStorage, LiteDbStorage.PersistentStorage>();
    }
}