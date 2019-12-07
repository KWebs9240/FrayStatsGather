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
    public class NotDeadMsgHandler : BaseTeamsMsgHandler
    {
        public override string HandlerName => "NotDead";

        public override async Task<bool> DoHandleMessage(ConnectorClient connector, Activity activity)
        {
            Activity reply = activity.CreateReply("Confirmed"); ;

            await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);

            return true;
        }

        public override bool DoMessageTrigger(Activity activity)
        {
            if (activity.Type.Equals("message"))
            {
                return activity.GetTextWithoutMentions().ToLower().Equals("confirm not dead");
            }

            return false;
        }
    }
}