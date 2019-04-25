using Microsoft.Extensions.DependencyInjection;
using DiscordBot.Discord;

namespace DiscordBot.ConsoleApp
{
    public class InversionOfControl
    {
        private static ServiceProvider provider;

        public static ServiceProvider Provider
        {
            get
            {
                return GetOrInitProvider();
            }
        }

        private static ServiceProvider GetOrInitProvider()
        {
            if (provider is null)
            {
                InitializeProvider();
            }

            return provider;
        }

        private static void InitializeProvider()
            => provider = new ServiceCollection()
            .AddSingleton<IDiscordBotClient, DiscordBotClient>()
            .BuildServiceProvider();
    }
}
