using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Discord.CommandModules
{
    [Group("Stats")]
    public class StatsModule : ModuleBase<SocketCommandContext>
    {
        [Command("Hour")]
        public async Task Hour(string channelName)
        {
            var time = DateTime.Now.ToString(@"h\:mm");


        }
    }
}
