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
    public class MeBlitzQuestionableMsgHandler : BaseTeamsMsgHandler
    {
        public override string HandlerName => "MeBlitzQuestionable";

        public async override Task<bool> DoHandleMessage(ConnectorClient connector, Activity activity)
        {
            IEnumerable<Mention> MentionsMinusBot = activity.Entities.Where(x => x.Type.Equals("mention")).Select(x => x.Properties.ToObject<Mention>()).Where(x => !x.Mentioned.Name.Equals("FraytaBot"));

            Activity reply = activity.CreateReply("Play your games\n\n");

            foreach (Mention blitz in MentionsMinusBot)
            {
                reply.AddMentionToText(blitz.Mentioned, MentionTextLocation.AppendText);
            }

            for (int i = 0; i < 10; i++)
            {
                await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
            }

            return true;
        }

        public override bool DoMessageTrigger(Activity activity)
        {
            if (activity.Type.Equals("message"))
            {
                HashSet<string> usersWhoCanBlitz = new HashSet<string>() { "kyle webster", "george wu", "thomas walter" };

                return usersWhoCanBlitz.Contains(activity.From.Name.ToLower())
                    && activity.GetTextWithoutMentions().ToLower().Contains("blitz");
            }

            return false;
        }
    }
}