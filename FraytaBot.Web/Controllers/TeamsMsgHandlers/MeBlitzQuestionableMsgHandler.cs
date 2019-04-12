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
    public class MeBlitzQuestionableMsgHandler : ITeamsMsgHandler
    {
        public async Task HandleMessage(ConnectorClient connector, Activity activity)
        {
            IEnumerable<Mention> MentionsMinusBot = activity.Entities.Where(x => x.Type.Equals("mention")).Select(x => x.Properties.ToObject<Mention>()).Where(x => !x.Mentioned.Name.Equals("FraytaBot"));

            Activity reply = activity.CreateReply("Play your games\n\n");

            foreach(Mention blitz in MentionsMinusBot)
            {
                reply.AddMentionToText(blitz.Mentioned, MentionTextLocation.AppendText);
            }

            for (int i = 0; i < 10; i++)
            {
                await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
            }
        }

        public bool MessageTrigger(Activity activity)
        {
            if(activity.Type.Equals("message"))
            {
                return activity.From.Name.ToLower().Equals("kyle webster")
                    && activity.GetTextWithoutMentions().ToLower().Contains("blitz");
            }

            return false;
        }
    }
}