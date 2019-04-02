using System;
using DiscordBotV1.Storage;

namespace DiscordBotV1
{
	internal class Program
	{
		private static void Main()
		{
			Unity.RegisterTypes();

			var a = Unity.Resolve<MyProfile>();

			Console.WriteLine("Hello, Discord!");
		}
	}

	public class MyProfile
	{
		private readonly IDataStorage _storage;

		public MyProfile(IDataStorage storage)
		{
			_storage = storage;
		}

		public void NewUser(string name)
		{
			var registrationTime = DateTime.UtcNow;

			_storage.StoreObject(registrationTime, name);
		}
	}
}
