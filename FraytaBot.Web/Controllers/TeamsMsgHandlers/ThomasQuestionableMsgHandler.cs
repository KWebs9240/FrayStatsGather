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
    public class ThomasQuestionableMsgHandler : BaseTeamsMsgHandler
    {
        public override string HandlerName => "ThomasQuestionable";

        public async override Task<bool> DoHandleMessage(ConnectorClient connector, Activity activity)
        {
            Activity reply = activity.CreateReply("Sir... I'm going to need you to settle down");

            await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);

            return true;
        }

        public override bool DoMessageTrigger(Activity activity)
        {
            if (activity.Type.Equals("message"))
            {
                return activity.From.Name.ToLower().Equals("thomas walter");
            }

            return false;
        }
    }
}