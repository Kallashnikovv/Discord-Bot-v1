using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Discord.Entities.Logging;

namespace DiscordBotBot.DiscordBotDiscord.Converters
{
    public static class DiscordBotEntityConverter
    {
        public static DiscordBotLog CovertLog(LogMessage logMessage)
            => new DiscordBotLog
            {
                Message = logMessage.Message,
                Source = logMessage.Source,
                Severity = ConvertSevrity(logMessage.Severity)
            };

        private static DiscordBotLogSeverity ConvertSevrity(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Critical:
                    return DiscordBotLogSeverity.Critical;
                case LogSeverity.Error:
                    return DiscordBotLogSeverity.Error;
                case LogSeverity.Warning:
                    return DiscordBotLogSeverity.Warning;
                case LogSeverity.Info:
                    return DiscordBotLogSeverity.Info;
                default:
                    return DiscordBotLogSeverity.Info;
            }
        }

        public static DiscordBotCommandLog ConvertCommandLog(SocketGuild guild, SocketGuildChannel channel, SocketGuildUser user, CommandInfo command)
            => new DiscordBotCommandLog
            {
                Channel = channel.Name,
                User = $"{user.Username}#{user.Discriminator}",
                Guild = guild.Name,
                CommandName = command.Name
            };

    }
}