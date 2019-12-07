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
    public class ActionFinderMsgHandler : BaseTeamsMsgHandler
    {
        public override string HandlerName => "ActionFinder";

        public async override Task<bool> DoHandleMessage(ConnectorClient connector, Activity activity)
        {
            Activity reply = activity.CreateReply($"Activity Type: {activity.Type}\n\nPerformed By: {activity.From.Name}\n\nProperties: {JsonConvert.SerializeObject(activity.Properties)}\n\nFrom Id: {activity.From.Id}\n\nActivity Id: {activity.Id}\n\nConversation Id: {activity.Conversation.Id}");

            await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);

            return true;
        }

        public override bool DoMessageTrigger(Activity activity)
        {
            return true;
        }
    }
}