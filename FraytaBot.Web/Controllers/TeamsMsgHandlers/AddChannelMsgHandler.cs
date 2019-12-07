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
    public class AddChannelMsgHandler : BaseTeamsMsgHandler
    {
        public override string HandlerName => "AddChannel";

        public async override Task<bool> DoHandleMessage(ConnectorClient connector, Activity activity)
        {
            bool rejected = true;
            ChallongeSQLHelper.ChallongeSQLHelperConnectionString = ConfigurationManager.AppSettings["dbConnection"];
            var channelThing = activity.GetChannelData<TeamsChannelData>();

            FrayDbTeamsTeam existingTeam = ChallongeSQLHelper.SqlGetSingleTeam(channelThing.Team.Id);
            if (existingTeam == null)
            {
                var teamInfo = connector.GetTeamsConnectorClient().Teams.FetchTeamDetails(channelThing.Team.Id);

                var addTeam = new FrayDbTeamsTeam()
                {
                    TeamId = teamInfo.Id,
                    TeamName = teamInfo.Name
                };

                ChallongeSQLHelper.SqlSaveTeam(addTeam);
                rejected = false;
            }

            //Check to see whether we've already got the channel
            FrayDbTeamsChannel existingChannel = ChallongeSQLHelper.SqlGetSingleChannel(channelThing.Channel.Id, channelThing.Team.Id);
            if (existingChannel == null)
            {
                //If your channel id matches the team id, you're in the autogenerate General
                if (channelThing.Channel.Id.Equals(channelThing.Team.Id))
                {
                    var addChannel = new FrayDbTeamsChannel()
                    {
                        ChannelId = channelThing.Channel.Id,
                        TeamId = channelThing.Team.Id,
                        ChannelName = "General",
                        IsPost = true
                    };

                    ChallongeSQLHelper.SqlSaveChannel(addChannel);
                    rejected = false;
                }
                else
                {
                    //We gotta get the channels and figure out which one we're in
                    var convoList = connector.GetTeamsConnectorClient().Teams.FetchChannelList(channelThing.Team.Id);
                    var currentChannel = convoList.Conversations.First(x => x.Id.Equals(channelThing.Channel.Id));

                    var addChannel = new FrayDbTeamsChannel()
                    {
                        ChannelId = currentChannel.Id,
                        TeamId = channelThing.Team.Id,
                        ChannelName = currentChannel.Name,
                        IsPost = true
                    };

                    ChallongeSQLHelper.SqlSaveChannel(addChannel);
                    rejected = false;
                }
            }

            Activity reply = null;

            if (rejected)
            {
                reply = activity.CreateReply("That's going to be a hard pass");
            }
            else
            {
                reply = activity.CreateReply("Saved the data");
            }

            await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);

            return true;
        }

        public override bool DoMessageTrigger(Activity activity)
        {
            if (activity.Type.Equals("message"))
            {
                return activity.GetTextWithoutMentions().ToLower().Equals("over here");
            }

            return false;
        }
    }
}