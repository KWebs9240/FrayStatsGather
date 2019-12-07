﻿using System;
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
    public class TestingIdsMsgHandler : BaseTeamsMsgHandler
    {
        public override string HandlerName => "TestingIds";

        public async override Task<bool> DoHandleMessage(ConnectorClient connector, Activity activity)
        {
            var channelThing = activity.GetChannelData<TeamsChannelData>();

            Activity reply = activity.CreateReply();
            reply.Text = $"Activity.ChannelId = {activity.ChannelId}\n\nchannelThing.Channel.ChannelId = {channelThing.Channel.Id}";

            await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);

            return true;
        }

        public override bool DoMessageTrigger(Activity activity)
        {
            if (activity.Type.Equals("message"))
            {
                return activity.GetTextWithoutMentions().ToLower().Equals("this is bullshi...");
            }

            return false;
        }
    }
}