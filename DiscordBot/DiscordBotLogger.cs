using DiscordBot.Discord.Entities.Logging;
using System;
using System.Threading.Tasks;

namespace DiscordBot
{
    class DiscordBotLogger : ILogger
    {
        public async Task LogAsync(DiscordBotLog logMessage)
        {
            await Append($"{ConvertSource(logMessage.Source)} ", ConsoleColor.DarkGray);
            await Append($"[{logMessage.Severity}] ", await SeverityColor(logMessage.Severity));
            await Append($"{logMessage.Message}\n", ConsoleColor.White);
        }

        public async Task LogCriticalAsync(DiscordBotLog logMessage, Exception exception)
        {
            await Append($"{ConvertSource(logMessage.Source)} ", ConsoleColor.DarkGray);
            await Append($"[{logMessage.Severity}] ", await SeverityColor(logMessage.Severity));
            await Append($"{logMessage.Message}\n", ConsoleColor.White);
            await Append($"{exception.Message}", ConsoleColor.DarkGray);
        }

        public async Task LogCommandAsync(DiscordBotCommandLog log)
        {
            await Append("DISC ", ConsoleColor.DarkGray);
            await Append("[CMND] ", ConsoleColor.Magenta);
            await Append($"Command {log.CommandName} Executed For {log.User} in {log.Guild}/#{log.Channel}\n", ConsoleColor.White);
        }

        public async Task LogCommandAsync(DiscordBotCommandLog log, string error)
        {
            await Append("DISC ", ConsoleColor.DarkGray);
            await Append("[CMND] ", ConsoleColor.Magenta);
            await Append($"Command ERROR: {error} For {log.User} in {log.Guild}/#{log.Channel}\n", ConsoleColor.White);
        }

        private async Task Append(string message, ConsoleColor color)
        {
            await Task.Run(() => {
                Console.ForegroundColor = color;
                Console.Write(message);
                return Task.CompletedTask;
            });
        }

        private string ConvertSource(string source)
        {
            switch (source.ToLower())
            {
                case "discord":
                    return "DISC";
                case "gateway":
                    return "GTWY";
                case "command":
                    return "COMD";
                case "rest":
                    return "REST";
                default:
                    return source;
            }
        }

        private Task<ConsoleColor> SeverityColor(DiscordBotLogSeverity severity)
        {
            switch (severity)
            {
                case DiscordBotLogSeverity.Critical:
                    return Task.FromResult(ConsoleColor.Red);
                case DiscordBotLogSeverity.Error:
                    return Task.FromResult(ConsoleColor.DarkRed);
                case DiscordBotLogSeverity.Warning:
                    return Task.FromResult(ConsoleColor.Yellow);
                case DiscordBotLogSeverity.Info:
                    return Task.FromResult(ConsoleColor.Green);
                default:
                    return Task.FromResult(ConsoleColor.DarkGray);
            }
        }
    }
}
