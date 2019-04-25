using DiscordBot.Core.Entities.Logging;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Core.Services.Logger
{
    public interface ILogger
    {
        Task LogAsync(DiscordBotLog logMessage);
        Task LogCriticalAsync(DiscordBotLog logMessage, Exception exception);
        Task LogCommandAsync(DiscordBotCommandLog log);
        Task LogCommandAsync(DiscordBotCommandLog log, string error);
    }
}
