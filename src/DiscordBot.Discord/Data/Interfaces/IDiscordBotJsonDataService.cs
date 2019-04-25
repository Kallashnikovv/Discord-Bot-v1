using System.Threading.Tasks;

namespace DiscordBot.Discord.Data.Interfaces
{
    interface IDiscordBotJsonDataService
    {
        Task<T> Retreive<T>(string path);
        Task Save(object obj, string path);
        bool FileExists(string path);
    }
}
