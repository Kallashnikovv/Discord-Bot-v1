using DiscordBot.Discord;
using System.Threading.Tasks;

namespace DiscordBot
{
	internal class Program
	{
		private static async Task Main()
		{
            var start = new DiscordBotClient();

            await start.InitializeAsync();
		}
	}
}
