using System;
using DiscordBotV1.Storage;
using DiscordBotV1.Discord;
using DiscordBotV1.Discord.Entities;
using DiscordBotV1.Discord.Handlers;
using System.Threading.Tasks;

namespace DiscordBotV1
{
	internal class Program
	{
		private static async Task Main()
		{
			Unity.RegisterTypes();
			Console.WriteLine("Hello, Discord!");		

			var storage = Unity.Resolve<IDataStorage>();

			var connection = Unity.Resolve<Connection>();
			await connection.ConnectAsync(new BotConfig
			{
				Token = storage.RestoreObject<string>("Config/BotToken"),
			});
		}
	}
}
