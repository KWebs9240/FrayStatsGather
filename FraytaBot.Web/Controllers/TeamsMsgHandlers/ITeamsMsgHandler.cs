using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrataBot.Web.Controllers.TeamsMsgHandlers
{
    public interface ITeamsMsgHandler
    {
        bool MessageTrigger(Activity activity);
        Task HandleMessage(ConnectorClient connector, Activity activity);
    }
}
