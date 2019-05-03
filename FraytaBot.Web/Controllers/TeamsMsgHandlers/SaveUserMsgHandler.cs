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
            var channelThing = activity.GetChannelData<TeamsChannelData>();
            ChallongeSQLHelper.ChallongeSQLHelperConnectionString = ConfigurationManager.AppSettings["dbConnection"];

            FrayDbTeamsUser teamsUser = ChallongeSQLHelper.SqlGetSingleUser(activity.From.Id, channelThing.Channel.Id);

            bool existingUser = teamsUser != null;

            teamsUser = teamsUser ?? new FrayDbTeamsUser() { UserId = activity.From.Id, UserName = activity.From.Name };
            teamsUser.IsTag = true;
            teamsUser.ChannelId = channelThing.Channel.Id;

            //if(existingUser) { ChallongeSQLHelper.SqlUpdateTeamsUser(activity.From.Id, teamsUser); }
            if(!existingUser) { ChallongeSQLHelper.SqlInsertTeamsUser(teamsUser); }

            Activity reply = activity.CreateReply();
            if(existingUser)
            {
                reply.AddMentionToText(activity.From, MentionTextLocation.AppendText);
            }
            else
            {
                reply.Text = $"Added information for {activity.From.Name}";
            }
            
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