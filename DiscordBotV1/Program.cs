using System;
using DiscordBotV1.Storage;
using DiscordBotV1.Discord;
using DiscordBotV1.Discord.Entities;
using System.Threading.Tasks;

namespace DiscordBotV1
{
	internal class Program
	{
		private static async Task Main()
		{
			Unity.RegisterTypes();
			Console.WriteLine("Hello, Discord!");

            var storage = Unity.Resolve<IDataStorage>(null, new {val=1});
            var connection = Unity.Resolve<Connection>(null, new {val=1});

            var botConfig = storage.RestoreObject<BotConfig>("Config/Config");

            await connection.ConnectAsync(botConfig);
		}
	}
}
