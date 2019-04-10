using Newtonsoft.Json;

namespace DiscordBotV1.Discord.Entities
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
