using Discord;
using Discord.Commands;

namespace DiscordBot.Discord
{
    public class CommandServicesConfig
    {
        public static CommandServiceConfig GetDefault()
        {
            return new CommandServiceConfig
            {
                LogLevel = LogSeverity.Debug,
                CaseSensitiveCommands = false
            };
        }

        public static CommandService GetNew()
        {
            return new CommandService();
        }
    }
}
