using DiscordBot.Data.Entities;

namespace DiscordBot.Discord
{
    public static class Global
    {
        public static string ResourcesFolder { get; set; } = "./Resources";
        public static string ConfigPath { get; set; } = $"./{ResourcesFolder}/Config.json";
        public static BotConfig BotConfig { get; set; }
    }
}
