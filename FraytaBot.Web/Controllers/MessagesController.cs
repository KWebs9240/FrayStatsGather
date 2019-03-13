using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ChallongeEntities;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using Microsoft.Bot.Connector.Teams.Models;

namespace FraytaBot.Web.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            using (var connector = new ConnectorClient(new Uri(activity.ServiceUrl)))
            {
                await TestAlive(connector, activity);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
        }

        public static async Task TestAlive(ConnectorClient connector, Activity activity)
        {
            try
            {
                if(!activity.Type.Equals("message"))
                {
                    return;
                }

                List<TournamentRetrieval> recentTournaments = ChallongeApiHelper.HttpHelper.ChallongeHttpHelper.GetRecentTournaments();

                var final = recentTournaments.Last();

                var channelThing = activity.GetChannelData<TeamsChannelData>();

                //Not going to be able to tag a team or channel until this is potenitally fixed
                //https://github.com/OfficeDev/BotBuilder-MicrosoftTeams/issues/139
                Activity reply = activity.CreateReply($"The most recent tournament I probably found is\n\n[Cash Money]({final.sign_up_url}) ");

                
                await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
            }
            catch(Exception ex)
            {
                Activity reply = activity.CreateReply(ex.Message);

                await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
            }
        }
    }
}
