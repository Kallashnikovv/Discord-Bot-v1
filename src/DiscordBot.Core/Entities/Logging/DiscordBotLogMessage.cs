namespace DiscordBot.Core.Entities.Logging
{
    public class DiscordBotLog
    {
        public string Source { get; set; }
        public string Message { get; set; }
        public DiscordBotLogSeverity Severity { get; set; }
    }
}