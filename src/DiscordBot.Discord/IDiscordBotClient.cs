using System.Threading.Tasks;

namespace DiscordBot.Discord
{
    public interface IDiscordBotClient
    {
        Task InitializeAsync();
    }
}
