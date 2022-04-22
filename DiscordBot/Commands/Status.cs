using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class Status : BaseCommandModule
    {
        [Command("status")]
        [RequireRoles(RoleCheckMode.MatchNames, "VEGA", "Slayer")]
        public async Task SetStatus(CommandContext ctx, [Description("0 pro Hraní, 1 pro Streamování, 2 pro Poslouchání, 3 pro Sledování, 4 pro Vlastní, 5 pro Kompetitivní")] int statusType, string statusMessage)
        {
            int[] activityTypes = { 0, 1, 2, 3, 4, 5 };
            DiscordActivity activity = new DiscordActivity();

            DiscordClient discord = ctx.Client;

            activity.Name = statusMessage;

            switch (statusType)
            {
                case 0:
                    activity.ActivityType = ActivityType.Playing;
                    break;
                case 1:
                    activity.ActivityType = ActivityType.Streaming;
                    break;
                case 2:
                    activity.ActivityType = ActivityType.ListeningTo;
                    break;
                case 3:
                    activity.ActivityType = ActivityType.Watching;
                    break;
                case 4:
                    activity.ActivityType = ActivityType.Custom;
                    break;
                case 5:
                    activity.ActivityType = ActivityType.Competing;
                    break;
                default:
                    activity.ActivityType = ActivityType.ListeningTo;
                    activity.Name = "Tvoji mámu";
                    break;
            }

            if (statusMessage == "delete")
            {
                activity.Name = String.Empty;
            }

            await discord.UpdateStatusAsync(activity);
        }
    }
}
