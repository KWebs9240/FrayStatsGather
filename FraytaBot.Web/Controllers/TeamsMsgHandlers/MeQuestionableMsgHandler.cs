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
    public class MeQuestionableMsgHandler : ITeamsMsgHandler
    {
        public async Task HandleMessage(ConnectorClient connector, Activity activity)
        {
            Activity reply = activity.CreateReply("So that worked");

            await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
        }

        public bool MessageTrigger(Activity activity)
        {
            if(activity.Type.Equals("message"))
            {
                return activity.From.Name.ToLower().Equals("kyle webster");
            }

            return false;
        }
    }
}