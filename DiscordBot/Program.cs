using DiscordBot.Discord;
using DiscordBot.Discord.Entities;
using System.Threading.Tasks;

namespace DiscordBot
{
	internal class Program
	{
		private static async Task Main()
		{
			Unity.RegisterTypes();

            var storage = Unity.Resolve<IDataStorage>(null, new {val=1});
            var connection = Unity.Resolve<Connection>(null, new {val=1});

            var botConfig = storage.RestoreObject<BotConfig>("Config/Config");

            Global.BotConfig = botConfig;

            await connection.ConnectAsync();
		}
	}
}
