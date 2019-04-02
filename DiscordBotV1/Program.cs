using System;
using DiscordBotV1.Discord;
using DiscordBotV1.Discord.Entities;

namespace DiscordBotV1
{
	internal class Program
	{
		private static void Main()
		{
			Unity.RegisterTypes();
			Console.WriteLine("Hello, Discord!");

			var discordBotConfig = new BotConfig
			{
				Token = "ABC",
				SocketConfig = SocketConfig.GetDefault()
			};
		}
	}
}
