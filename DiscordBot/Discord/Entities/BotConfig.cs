using Newtonsoft.Json;

namespace DiscordBot.Discord.Entities
{
	public class BotConfig
	{
        [JsonProperty("Token")]
		public string Token { get; set; }
        [JsonProperty("Prefix")]
		public string Prefix { get; set; }
        [JsonProperty("NowPlaying")]
		public string NowPlaying { get; set; }
	}
}
