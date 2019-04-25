using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBotBot.DiscordBotDiscord.Converters;

namespace DiscordBot.Discord.Handlers
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;
        private readonly IServiceProvider _services;
        private readonly ILogger _logger;

        public CommandHandler(DiscordSocketClient client, CommandService commandService, IServiceProvider services, ILogger logger)
        {
            _client = client;
            _commandService = commandService;
            _services = services;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

            _client.MessageReceived += HandleCommandAsync;
            _commandService.CommandExecuted += CommandExecutedsync;
            _commandService.Log += LogAsync;
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

        private async Task CommandExecutedsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!command.IsSpecified)
                return;
            var commandLog = DiscordBotEntityConverter.ConvertCommandLog(context.Guild as SocketGuild, context.Channel as SocketGuildChannel, context.User as SocketGuildUser, command.Value);
            if (result.IsSuccess)
            {
                await _logger.LogCommandAsync(commandLog);
            }
            else
            {
                await _logger.LogCommandAsync(commandLog, result.ErrorReason);
                var embed = new EmbedBuilder()
                    .WithTitle("ERROR")
                    .WithDescription(result.ErrorReason)
                    .WithColor(Color.DarkRed);

                await context.Channel.SendMessageAsync(embed: embed.Build());
            }
        }

        private async Task LogAsync(LogMessage logMessage)
        {
            var DiscordBotLog = DiscordBotEntityConverter.CovertLog(logMessage);
            await _logger.LogAsync(DiscordBotLog);
        }

    }
}