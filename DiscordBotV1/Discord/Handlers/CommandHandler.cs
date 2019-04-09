using System;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBotV1.Discord.Handlers
{
    public class CommandHandler : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient _client;
        private CommandService _commandService;
        internal StringBuilder LogMessage = new StringBuilder();

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client ?? new DiscordSocketClient(new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100
            });
            _commandService = new CommandService();
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            _client.MessageReceived += HandleCommandAsync;
            //_client.Log += Client_Log;
            _client.MessageReceived += ClientCom_Log;
            _client.MessageDeleted += MsgDelClient_Log;
            //_client.MessageUpdated += MsgEdtClient_Log;
        }

        private async Task ClientCom_Log(SocketMessage msg)
        {
            var guild = _client.GetGuild(561306978484355073); // server id here
            var channel = guild.GetTextChannel(564491145325969428); // channel id
            var embed = new EmbedBuilder();
            embed.WithColor(255, 235, 25);

            if (!msg.Author.IsBot)
            {
                embed.WithAuthor(msg.Author.Username + "#" + msg.Author.Discriminator);
                embed.WithTimestamp(msg.CreatedAt);
                embed.WithFooter("ID:" + msg.Id);
                embed.AddField("User Id: " + msg.Author.Id.ToString(), null);
                embed.WithDescription(msg.ToString());
                await channel.SendMessageAsync("", false, embed.Build());
            }
        }

        private async Task MsgDelClient_Log(Cacheable<IMessage, ulong> cachedMessage, ISocketMessageChannel chnl)
        {
            var guild = _client.GetGuild(561306978484355073); // server id here
            var channel = guild.GetTextChannel(565255635227246609); // channel id
            var embed = new EmbedBuilder();
            embed.WithColor(221, 95, 83);

            if (cachedMessage.Value is null) return;

            var msg = cachedMessage.Value;

            embed.WithAuthor(msg.Author.Username + "#" + msg.Author.Discriminator);
            embed.WithFooter("ID: " + msg.Id);
            embed.WithTimestamp(msg.CreatedAt);
            embed.AddField("User Id: " + msg.Author.Id.ToString(), null);
            embed.WithDescription(msg.ToString());
            await channel.SendMessageAsync("", false, embed.Build());
        }


        /*private async Task Client_Log(Cacheable<IMessage, ulong> cachedMessage, ISocketMessageChannel chnl)
        {
            var guild = _client.GetGuild(561306978484355073); // server id here
            var channel = guild.GetTextChannel(564491145325969428); // channel id
            var embed = new EmbedBuilder();
            embed.WithColor(255, 235, 25);

            LogMessage.Append(msg.Message);
            LogMessage.Append("\n");

            await Task.Delay(3000);
            if (LogMessage.Length >= 25)
            {
                embed.WithDescription(LogMessage.ToString());
                await channel.SendMessageAsync("", false, embed.Build());
                LogMessage.Clear();
            }
            await Task.Delay(3000);
        }*/

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
                } else if(msg.Content == "chlebek" || msg.Content == "kebab" || msg.Content == "test")
                {
                    var result = await _commandService.ExecuteAsync(context, argPos, null);
                    if(!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                    {
                        Console.WriteLine(result.ErrorReason);
                    }
                }
        }

        [Command("playing")]
        [RequireOwner]
        public async Task SetGame(ActivityType activityType, [Remainder]string game)
        {
            await _client.SetGameAsync(game, null, activityType);
        }
    }
}