using System;
using System.Threading.Tasks;
using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBotV1.Discord.Entities;

namespace DiscordBotV1.Discord.Handlers
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _commandService;
        
        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;
            _commandService = new CommandService();
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if(msg == null) return;
            var context = new SocketCommandContext(_client, msg);
            int argPos = 0;
            if(msg.HasStringPrefix("!", ref argPos)
                || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
                {
                    var result = await _commandService.ExecuteAsync(context, argPos, null);
                    if(!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                    {
                        Console.WriteLine(result.ErrorReason);
                    }
                } else if(msg.Content == "chlebek" || msg.Content == "kebab")
                {
                    var result = await _commandService.ExecuteAsync(context, argPos, null);
                    if(!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                    {
                        Console.WriteLine(result.ErrorReason);
                    }
                }
        }
    }
}