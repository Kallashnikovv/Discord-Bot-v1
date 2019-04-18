using Unity;
using Unity.Lifetime;
using Unity.Injection;
using Unity.Resolution;
using Discord.WebSocket;
using DiscordBotV1.Discord;
using DiscordBotV1.Storage;
using DiscordBotV1.Storage.Implementations;
using System.Reflection;
using System.Linq;
using Victoria;
using Discord.Commands;

namespace DiscordBotV1
{
	public static class Unity
	{
		private static UnityContainer _container;

		public static UnityContainer Container
		{
			get
			{
				if (_container == null)
					RegisterTypes();
				return _container;
			}
		}

		public static void RegisterTypes()
		{
			_container = new UnityContainer();
			_container.RegisterSingleton<IDataStorage, JsonStorage>();
			_container.RegisterSingleton<ILogger, Logger>();
			_container.RegisterType<DiscordSocketConfig>(new InjectionFactory(i => SocketConfig.GetDefault())); //TODO: Exchange to IUnityContainer
			_container.RegisterType<CommandServiceConfig>(new InjectionFactory(i => CommandServicesConfig.GetDefault())); //TODO: Exchange to IUnityContainer
			_container.RegisterSingleton<DiscordSocketClient>(new InjectionConstructor(typeof(DiscordSocketConfig)));
			_container.RegisterSingleton<Connection>();
		}

		public static T Resolve<T>(this IUnityContainer container, object ParameterOverrides)
		{
			var properties = ParameterOverrides
				.GetType()
				.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var overridesArray = properties
				.Select(p => new ParameterOverride(p.Name, p.GetValue(ParameterOverrides, null)))
				.Cast<ResolverOverride>()
				.ToArray();
			return Container.Resolve<T>(null, overridesArray);
		}
	}
}
