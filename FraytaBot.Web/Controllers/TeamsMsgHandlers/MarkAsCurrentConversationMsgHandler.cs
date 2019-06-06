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
using Newtonsoft.Json;

namespace FrataBot.Web.Controllers.TeamsMsgHandlers
{
    public class MarkAsCurrentConversationMsgHandler : ITeamsMsgHandler
    {
        public async Task HandleMessage(ConnectorClient connector, Activity activity)
        {
            ChallongeSQLHelper.ChallongeSQLHelperConnectionString = ConfigurationManager.AppSettings["dbConnection"];
            ChallongeSQLHelper.SetCurrentConversation(activity.Conversation.Id);

            //Activity reply = activity.CreateReply($"{string.Join("\n\n", activity.Properties.Children().Select(x => x.Path))}");

            //Activity reply = activity.CreateReply($"Dead");

            //await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
        }

        public bool MessageTrigger(Activity activity)
        {
            if (activity.Type.Equals("messageReaction"))
            {
                return activity.From.Id.Equals("29:1NBWvrWJAn7xAH_Uk1xem14_YEmOBccFlXSU_I8gnsPpzJlLuzNC39L5_GIViKymEIxnumn0asG4UlDx9ilUksA")
                    && activity.Properties.Children().Any(x => x.Path.Contains("reactionsRemoved"));
            }

            return false;
        }
    }
}