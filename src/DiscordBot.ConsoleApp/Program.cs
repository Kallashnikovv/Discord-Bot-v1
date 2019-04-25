using DiscordBot.Discord;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DiscordBot.ConsoleApp
{
    public static class Program
    {
        static async Task Main()
        {
            await ActivatorUtilities.CreateInstance<DiscordBotClient>(InversionOfControl.Provider).InitializeAsync();
        }
    }
}
