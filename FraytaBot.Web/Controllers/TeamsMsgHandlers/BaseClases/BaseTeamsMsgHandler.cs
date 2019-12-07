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
    public abstract class BaseTeamsMsgHandler : ITeamsMsgHandler
    {
        public abstract string HandlerName { get; }
        public abstract bool DoMessageTrigger(Activity activity);
        public abstract Task<bool> DoHandleMessage(ConnectorClient connector, Activity activity);

        public async Task HandleMessage(ConnectorClient connector, Activity activity)
        {
            System.Diagnostics.Trace.TraceInformation($"Start: Handling {HandlerName}");

            bool SuccessfulExecute = await DoHandleMessage(connector, activity);

            if(SuccessfulExecute)
            {
                System.Diagnostics.Trace.TraceInformation($"Complete: Handling {HandlerName}");
            }
            else
            {
                System.Diagnostics.Trace.TraceInformation($"Failed: Handling {HandlerName}");
            }
        }

        public bool MessageTrigger(Activity activity)
        {
            System.Diagnostics.Trace.TraceInformation($"Start: Trigger {HandlerName}");


            bool TriggerResult = DoMessageTrigger(activity);

            if (TriggerResult)
            {
                System.Diagnostics.Trace.TraceInformation($"Complete True: Trigger {HandlerName}");
            }
            else
            {
                System.Diagnostics.Trace.TraceInformation($"Complete False: Trigger {HandlerName}");
            }

            return TriggerResult;
        }
    }
}