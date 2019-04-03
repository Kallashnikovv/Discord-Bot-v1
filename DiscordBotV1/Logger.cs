using System;

namespace DiscordBotV1
{
	public class Logger : ILogger
	{
		public void Log(string message)
		{
			Console.WriteLine(message);
		}
	}
}
