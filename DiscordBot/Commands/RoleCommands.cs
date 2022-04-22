using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class RoleCommands : BaseCommandModule
    {
        [Command("join")]
        [Description("Gives you the basic role")]
        public async Task AddMemberRole(CommandContext ctx)
        {
            var joinEmbed = new DiscordEmbedBuilder
            {
                Title = "Chceš se k nám připojit?",
                Color = DiscordColor.Cyan,
                Description = "Kliknutím na fajfku se staneš členem tohoto serveru!",
                Timestamp = DateTime.Now,
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = ctx.Client.CurrentUser.AvatarUrl }
            };

            var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed.WithAuthor("Jafaros")).ConfigureAwait(false);

            var YesEmoji = DiscordEmoji.FromName(ctx.Client, ":white_check_mark:");
            var NoEmoji = DiscordEmoji.FromName(ctx.Client, ":x:");

            await joinMessage.CreateReactionAsync(YesEmoji).ConfigureAwait(false);
            await joinMessage.CreateReactionAsync(NoEmoji).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();

            var result = await interactivity.WaitForReactionAsync(
                x => x.Message == joinMessage &&
                x.User == ctx.User &&
                (x.Emoji == YesEmoji || x.Emoji == NoEmoji)).ConfigureAwait(false);

            if (result.Result.Emoji == YesEmoji)
            {
                var role = ctx.Guild.GetRole(966775121392066623);
                await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);       
            }
            else if (result.Result.Emoji == NoEmoji)
            {
                await ctx.Channel.SendMessageAsync("Stále máš možnost získat roli později :wink:");
            }
            else 
            { 
                //
            }

            await ctx.Message.DeleteAsync().ConfigureAwait(false);
            await joinMessage.DeleteAsync().ConfigureAwait(false);
        }
    }
}
