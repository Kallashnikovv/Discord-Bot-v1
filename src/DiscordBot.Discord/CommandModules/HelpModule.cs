using Discord.Addons.CommandsExtension;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBot.Discord.CommandModules
{
    [Group, Name("Help")]
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commandService;
        
        public HelpModule(CommandService commandService)
        {
            _commandService = commandService;
        }

        [Command("Help")]
        public async Task Help([Remainder]string command = null)
        {
            var botPrefix = Global.BotConfig.Prefix;
            var helpEmbed = _commandService.GetDefaultHelpEmbed(command, botPrefix);
            await Context.Channel.SendMessageAsync(embed: helpEmbed);
        }
    }
}
