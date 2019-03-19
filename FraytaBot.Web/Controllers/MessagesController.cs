using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ChallongeEntities;
using FrataBot.Web.Controllers.TeamsMsgHandlers;
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
                foreach(ITeamsMsgHandler hand in _handlerList)
                {
                    try
                    {
                        if (hand.MessageTrigger(activity))
                        {
                            await hand.HandleMessage(connector, activity);
                        }
                    }
                    catch (Exception ex)
                    {
                        await YouBrokeIt(connector, activity, ex);
                    }
                }
                //await TestAlive(connector, activity);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
        }

        private static List<ITeamsMsgHandler> _handlerList = new List<ITeamsMsgHandler>()
        {
            new AddChannelMsgHandler()
        };

        public static async Task YouBrokeIt(ConnectorClient connector, Activity activity, Exception ex)
        {
            Activity reply = activity.CreateReply(ex.Message);
            reply.Text += $"\n\n{ex.StackTrace}";

            await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
        }
    }
}
