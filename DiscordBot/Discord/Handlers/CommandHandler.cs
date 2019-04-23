using System;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Discord.Handlers
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;
        private readonly IServiceProvider _services;
        private readonly DiscordLogger _logger;
        protected internal StringBuilder LogMessage = new StringBuilder();

        public CommandHandler(DiscordSocketClient client, CommandService commandService, IServiceProvider services, DiscordLogger logger)
        {
            _client = client;
            _commandService = commandService;
            _services = services;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            HookEvents();
        }

        internal void HookEvents()
        {
            _commandService.Log += _logger.Log;
            _client.MessageReceived += HandleCommandAsync;
            _client.MessageReceived += ClientCom_Log;
            _client.MessageDeleted += MsgDelClient_Log;
            //_client.MessageUpdated += MsgEdtClient_Log;
            _client.ReactionAdded += OnReactionAdded;
        }

        private async Task OnReactionAdded(Cacheable<IUserMessage, ulong> msg, ISocketMessageChannel channel, SocketReaction reaction)
        {
            
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null || msg.Author.IsBot) return;
            var context = new SocketCommandContext(_client, msg);
            int argPos = 0;
            if (msg.HasStringPrefix(Global.BotConfig.Prefix, ref argPos)
                || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _commandService.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
            else if (msg.Content.Contains("chlebek") || msg.Content.Contains("kebab") || msg.Content.Contains("papaj"))
            {
                var result = await _commandService.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
            if (msg.Author.Id.ToString() == "213637702375964682")
            {
                if (msg.Content.Contains("!cstatus"))
                {
                    if (msg.Content.Contains("0")) await _client.SetStatusAsync(UserStatus.Offline);
                    if (msg.Content.Contains("1")) await _client.SetStatusAsync(UserStatus.Online);
                    if (msg.Content.Contains("2")) await _client.SetStatusAsync(UserStatus.Idle);
                    if (msg.Content.Contains("3")) await _client.SetStatusAsync(UserStatus.AFK);
                    if (msg.Content.Contains("4")) await _client.SetStatusAsync(UserStatus.DoNotDisturb);
                    if (msg.Content.Contains("5")) await _client.SetStatusAsync(UserStatus.Invisible);
                }
            }
        }

        #region Events

        

        #endregion

        #region Logging stuff
        private async Task ClientCom_Log(SocketMessage msg)
        {
            var guild = _client.GetGuild(561306978484355073); // server id here
            var channel = guild.GetTextChannel(564491145325969428); // channel id
            var embed = new EmbedBuilder()
                .WithColor(255, 235, 25)
                .WithThumbnailUrl("https://i.gyazo.com/d51b75c8148b4c194d96e222f404a32b.png");

            var message = msg as SocketUserMessage;
            var server = msg.Channel as SocketGuildChannel;

            int argPos = 0;
            if (!msg.Author.IsBot && message.HasStringPrefix(Global.BotConfig.Prefix, ref argPos))
            {
                embed.WithAuthor("Command executed");
                embed.AddField($"{msg.Author}", $"User Id: {msg.Author.Id}");
                embed.AddField($"Guild: {server.Guild.Name}", $"Id: {server.Guild.Id}");
                embed.AddField($"Channel: #{msg.Channel.Name}", $"Id: {msg.Channel.Id}");
                embed.AddField($"Message content:", msg.Content);
                embed.WithFooter($"Message Id: {msg.Id}");
                embed.WithTimestamp(msg.CreatedAt);
                await channel.SendMessageAsync("", false, embed.Build());
            }
        }

        private async Task MsgDelClient_Log(Cacheable<IMessage, ulong> cachedMessage, ISocketMessageChannel chnl)
        {
            var guild = _client.GetGuild(561306978484355073); // server id here
            var channel = guild.GetTextChannel(565255635227246609); // channel id
            var embed = new EmbedBuilder()
                .WithColor(221, 95, 83)
                .WithThumbnailUrl("https://i.gyazo.com/b7b89f1f02f663a30d371f06f3ce00c8.png");

            if (cachedMessage.Value is null) return;

            var msg = cachedMessage.Value as SocketUserMessage;
            var server = msg.Channel as SocketGuildChannel;

            if (!msg.Author.IsBot && !msg.Content.Contains("!cls"))
            {
                embed.WithAuthor("Message deleted");
                embed.AddField($"{msg.Author}", $"User Id: {msg.Author.Id}");
                embed.AddField($"Guild: {server.Guild.Name}", $"Id: {server.Guild.Id}");
                embed.AddField($"Channel: #{msg.Channel.Name}", $"Id: {msg.Channel.Id}");
                embed.AddField($"Message content:", msg.Content);
                embed.WithFooter($"Message Id: {msg.Id}");
                embed.WithTimestamp(msg.CreatedAt);
                await channel.SendMessageAsync("", false, embed.Build());
            }
        }

        /*private async Task MsgEdtClient_Log(Cacheable<IMessage, ulong> cachedMessage, SocketMessage socketMessage, ISocketMessageChannel chnl)
        {
            var guild = _client.GetGuild(561306978484355073); // server id here
            var channel = guild.GetTextChannel(565255635227246609); // channel id
            var embed = new EmbedBuilder();
            embed.WithColor(221, 95, 83);

            Console.WriteLine(cachedMessage.Value.Content);

            if (cachedMessage.Value is null) return;

            var msg = cachedMessage.Value;
            if (!msg.Author.IsBot)
            {
                embed.WithAuthor(msg.Author.Username + "#" + msg.Author.Discriminator);
                embed.WithFooter("ID: " + msg.Id);
                embed.WithTimestamp(msg.CreatedAt);
                embed.AddField("User Id: " + msg.Author.Id.ToString(), "\u200b");
                embed.WithDescription(msg.Content);
                await channel.SendMessageAsync("", false, embed.Build());
            }
        }*/
        #endregion
    }
}