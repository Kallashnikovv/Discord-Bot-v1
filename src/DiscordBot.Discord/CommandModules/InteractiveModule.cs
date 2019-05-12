using Discord.Addons.Interactive;
using Discord.Commands;
using Discord;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace DiscordBot.Discord.CommandModules
{
    [Group, Name("Interactive")]
    public class InteractiveModule : InteractiveBase<SocketCommandContext>
    {
        [Command("PaginatorTest")]
        public async Task PaginatorTest()
        {
            var pages = new[]
            {
                new PaginatedMessage.Page
                {
                    Description = "Page1"
                },
                new PaginatedMessage.Page
                {
                    Author = new EmbedAuthorBuilder
                    {
                        IconUrl = Context.User.GetAvatarUrl(),
                        Name = Context.User.ToString(),
                        Url = Context.User.GetAvatarUrl()
                    },
                    Description = "Page 2 Description",
                    Title = "Page 2 Title",
                    Fields = new List<EmbedFieldBuilder>
                    {
                        new EmbedFieldBuilder
                        {
                            Name = "Field 1",
                            Value = "Field 1 Description"
                        }

                    },
                    ImageUrl = "https://discordapp.com/assets/9c38ca7c8efaed0c58149217515ea19f.png",
                    Color = Color.DarkMagenta,
                    FooterOverride = new EmbedFooterBuilder
                    {
                        IconUrl = Context.User.GetAvatarUrl(),
                        Text = Context.User.ToString()
                    },
                    ThumbnailUrl = Context.User.GetAvatarUrl(),
                    TimeStamp = DateTimeOffset.UtcNow,
                    Url = Context.User.GetAvatarUrl()
                },
                new PaginatedMessage.Page
                {
                    Author = new EmbedAuthorBuilder
                    {
                        IconUrl = Context.User.GetAvatarUrl(),
                        Name = Context.User.ToString(),
                        Url = Context.User.GetAvatarUrl()
                    },
                    Fields = new List<EmbedFieldBuilder>
                    {
                        new EmbedFieldBuilder
                        {
                            Name = "Field 1",
                            Value = "Field 1 Description"
                        }
                    },
                    Color = Color.DarkMagenta,
                    ThumbnailUrl = Context.User.GetAvatarUrl(),
                    TimeStamp = DateTimeOffset.UtcNow,
                    Url = Context.User.GetAvatarUrl()
                }
            };

            var pager = new PaginatedMessage
            {
                Pages = pages,
                Author = new EmbedAuthorBuilder
                {
                    IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                    Name = Context.Client.CurrentUser.ToString(),
                    Url = Context.Client.CurrentUser.GetAvatarUrl()
                },
                Color = Color.DarkGreen,
                Content = "Default Message Content",
                Description = "Default Embed Description",
                Fields = new List<EmbedFieldBuilder>
                {
                    new EmbedFieldBuilder
                    {
                        Name = "Default Field 1",
                        Value = "Default Field Desc 1"
                    }
                },
                FooterOverride = null,
                ImageUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                ThumbnailUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                Options = PaginatedAppearanceOptions.Default,
                TimeStamp = DateTimeOffset.UtcNow
            };


            await PagedReplyAsync(pager, new ReactionList
            {
                Forward = true,
                Backward = true,
                Jump = true,
                Trash = true
            });
        }
    }
}
