using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

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
            var channelThing = activity.GetChannelData<TeamsChannelData>();

            //Not going to be able to tag a team or channel until this is potenitally fixed
            //https://github.com/OfficeDev/BotBuilder-MicrosoftTeams/issues/139
            Activity reply = activity.CreateReply($"Testing Alive");

            await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
        }
    }
}
