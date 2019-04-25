namespace DiscordBot.Discord.Entities.Logging
{
    public class DiscordBotCommandLog
    {
        public string User { get; set; }
        public string CommandName { get; set; }
        public string Channel { get; set; }
        public string Guild { get; set; }
    }
}
