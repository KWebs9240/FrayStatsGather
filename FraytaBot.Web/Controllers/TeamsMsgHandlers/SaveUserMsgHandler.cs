using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ChallongeApiHelper.SQLHelper;
using FrayStatsDbEntities;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using Microsoft.Bot.Connector.Teams.Models;

namespace FrataBot.Web.Controllers.TeamsMsgHandlers
{
    public class SaveUserMsgHandler : ITeamsMsgHandler
    {
        public async Task HandleMessage(ConnectorClient connector, Activity activity)
        {
            FrayDbTeamsUser teamsUser = ChallongeSQLHelper.SqlGetSingleUser(activity.From.Id);

            bool existingUser = teamsUser != null;

            teamsUser = teamsUser ?? new FrayDbTeamsUser() { UserId = activity.From.Id, UserName = activity.From.Name };
            teamsUser.IsTag = true;

            if(existingUser) { ChallongeSQLHelper.SqlUpdateTeamsUser(activity.From.Id, teamsUser); }
            if(!existingUser) { ChallongeSQLHelper.SqlInsertTeamsUser(teamsUser); }

            Activity reply = activity.CreateReply();
            reply.Text = existingUser ? $"Updated information for {activity.From.Name}" : $"Added information for {activity.From.Name}";
            
            await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
        }

        public bool MessageTrigger(Activity activity)
        {
            if(activity.Type.Equals("message"))
            {
                return activity.GetTextWithoutMentions().ToLower().Equals("tag me");
            }

            return false;
        }
    }
}