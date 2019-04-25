using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Discord;
using System;

namespace DiscordBot.InversionOfControl
{
    public static class Container
    {
        public static IServiceCollection AddMiunieTypes(this IServiceCollection collection)
            => collection.AddSingleton<Random>()
                .AddSingleton<IDiscordBotClient, DiscordBotClient>();
        //.AddSingleton<IPersistentStorage, LiteDbStorage.PersistentStorage>()
    }
}