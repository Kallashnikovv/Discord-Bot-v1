using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Discord.Handlers
{
    public class LoggingHandler
    {

        private readonly DiscordSocketClient _client;
        protected internal StringBuilder LogMessage = new StringBuilder();

        public LoggingHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        public Task Initialize()
        {
            _client.MessageReceived += ClientCom_Log;
            _client.MessageDeleted += MsgDelClient_Log;
            _client.MessageUpdated += MsgEdtClient_Log;

            return Task.CompletedTask;
        }

        private async Task ClientCom_Log(SocketMessage msg)
        {
            var guild = _client.GetGuild(561306978484355073); // server id here
            var channel = guild.GetTextChannel(564491145325969428); // channel id
            int argPos = 0;

            var message = msg as SocketUserMessage;
            var server = msg.Channel as SocketGuildChannel;

            if (msg.Author.IsBot || !message.HasStringPrefix(Global.BotConfig.Prefix, ref argPos)) { return; }

            var embed = new EmbedBuilder()
                .WithColor(255, 235, 25)
                .WithThumbnailUrl("https://i.gyazo.com/d51b75c8148b4c194d96e222f404a32b.png")
                .WithAuthor("Command executed")
                .AddField($"{msg.Author}", $"User Id: {msg.Author.Id}")
                .AddField($"Guild: {server.Guild.Name}", $"Id: {server.Guild.Id}")
                .AddField($"Channel: #{msg.Channel.Name}", $"Id: {msg.Channel.Id}")
                .AddField($"Message content:", msg.Content)
                .WithFooter($"Message Id: {msg.Id}")
                .WithTimestamp(msg.CreatedAt)
                .Build();

            await channel.SendMessageAsync("", false, embed);
        }

        private async Task MsgDelClient_Log(Cacheable<IMessage, ulong> cachedMessage, ISocketMessageChannel chnl)
        {
            var logGuild = _client.GetGuild(561306978484355073); // server id here
            var logChannel = logGuild.GetTextChannel(565255635227246609); // channel id

            var msg = cachedMessage.Value as SocketUserMessage;
            var server = msg.Channel as SocketGuildChannel;

            if (!(cachedMessage.Value is null || msg.Author.IsBot is true || msg.Author.IsWebhook is true || msg.Content.Contains("!cls") is true))
            {
                var embed = new EmbedBuilder()
                    .WithColor(221, 95, 83)
                    .WithThumbnailUrl("https://i.gyazo.com/b7b89f1f02f663a30d371f06f3ce00c8.png")
                    .WithAuthor("Message deleted")
                    .AddField($"{msg.Author}", $"User Id: {msg.Author.Id}")
                    .AddField($"Guild: {server.Guild.Name}", $"Id: {server.Guild.Id}")
                    .AddField($"Channel: #{msg.Channel.Name}", $"Id: {msg.Channel.Id}")
                    .AddField($"Message content:", msg.Content)
                    .WithFooter($"Message Id: {msg.Id}")
                    .WithTimestamp(msg.CreatedAt)
                    .Build();

                await logChannel.SendMessageAsync("", false, embed);
            }
        }

        private async Task MsgEdtClient_Log(Cacheable<IMessage, ulong> cachedMessage, SocketMessage socketMessage, ISocketMessageChannel channel)
        {
            var logGuild = _client.GetGuild(561306978484355073); // server id here
            var logChannel = logGuild.GetTextChannel(565255635227246609); // channel id

            if (cachedMessage.Value is null || socketMessage is null || channel is null) { return; }

            var oldMsg = cachedMessage.Value as SocketUserMessage;
            var newMsg = socketMessage as SocketUserMessage;
            var chnl = channel as SocketGuildChannel;

            var embed = new EmbedBuilder()
                .WithColor(110, 215, 30)
                .WithThumbnailUrl("https://i.gyazo.com/0a72e17fffc13ef4ae7379592d7663ac.png")
                .WithAuthor("Message edited")
                .AddField($"{oldMsg.Author}", $"User Id: {oldMsg.Author.Id}")
                .AddField($"Guild: {chnl.Guild.Name}", $"Id: {chnl.Guild.Id}")
                .AddField($"Channel: #{chnl.Name}", $"Id: {chnl.Id}")
                .AddField($"Old message content:", oldMsg.Content)
                .AddField($"New message content:", newMsg.Content)
                .WithFooter($"Message Id: {oldMsg.Id}")
                .WithTimestamp(oldMsg.EditedTimestamp.Value)
                .Build();

            await logChannel.SendMessageAsync("", false, embed);
        }
    }
}
