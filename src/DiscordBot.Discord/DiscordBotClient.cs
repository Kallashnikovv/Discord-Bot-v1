using System.Threading.Tasks;
using DiscordBot.Discord.Data.Entities;
using DiscordBot.Discord.Data.Interfaces;
using DiscordBot.Discord.Data;
using System;
using System.Threading;

namespace DiscordBot.Discord
{
    public class DiscordBotClient : IDiscordBotClient
    {
        private readonly IDiscordBotJsonDataService _dataServices;
        private BotConfig _config;

        private CancellationTokenSource _tokenSource;

        public DiscordBotClient()
        {
            _dataServices = _dataServices ?? new DiscordBotJsonDataService();
        }
        public async Task InitializeAsync()
        {
            Unity.RegisterTypes();
            var connection = Unity.Resolve<Connection>(null, new { val = 1 });

            _config = await InitializeConfigAsync();

            Global.BotConfig = _config;

            _tokenSource = new CancellationTokenSource();
            await connection.ConnectAsync(_tokenSource.Token);
        }

        private async Task<BotConfig> InitializeConfigAsync()
        {
            if (!_dataServices.FileExists(Global.ConfigPath))
                await _dataServices.Save(new BotConfig
                {
                    Token = "",
                    NowPlaying = "Change Me!",
                    Prefix = "!"
                }, Global.ConfigPath);

            var config = await _dataServices.Retreive<BotConfig>(Global.ConfigPath);

            if (string.IsNullOrWhiteSpace(config.Token))
            {
                Console.WriteLine("Please Enter Your Token: ");
                config.Token = Console.ReadLine();
                await _dataServices.Save(config, Global.ConfigPath);
                Console.Clear();
            }
            return config;
        }

    }
}
