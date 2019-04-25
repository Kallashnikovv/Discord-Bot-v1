using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Discord.CommandModules
{
    [Group("stats")]
    public class StatsModule : ModuleBase<SocketCommandContext>
    {
        [Command("hour")]
        public async Task Hour(string channelName)
        {
            var time = DateTime.Now.ToString(@"h\:mm");


        }
    }
}
