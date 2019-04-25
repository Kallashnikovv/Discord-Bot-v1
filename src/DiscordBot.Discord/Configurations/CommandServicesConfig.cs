using Discord;
using Discord.Commands;

namespace DiscordBot.Discord.Configurations
{
    public class CommandServicesConfig
    {
        public static CommandServiceConfig GetDefault()
        {
            return new CommandServiceConfig
            {
                LogLevel = LogSeverity.Debug,
                DefaultRunMode = RunMode.Async,
                CaseSensitiveCommands = false
            };
        }

        public static CommandService GetNew()
        {
            return new CommandService();
        }
    }
}
