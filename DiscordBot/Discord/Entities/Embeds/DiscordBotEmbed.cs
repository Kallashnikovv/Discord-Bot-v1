using System.Collections.Generic;

namespace DiscordBot.Discord.Entities.Embeds
{
    public class DiscordBotEmbed
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string Thumbnail { get; set; }
        public string Footer { get; set; }
        public List<Field> Fields { get; set; } = new List<Field>();
    }

    public class Field
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}