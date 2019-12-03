using System;
using System.Collections.Generic;
using ChallongeApiHelper.SQLHelper;
using FrayStatsDbEntities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using Microsoft.Bot.Connector.Teams.Models;
using Microsoft.Extensions.Logging;

namespace FrayFunctions
{
    public static class PostingFromFunction
    {
        [FunctionName("PostingFromFunction")]
        public static async void Run([TimerTrigger("0 0 15 * * 1")]TimerInfo myTimer, ILogger log)
        {
            ChallongeSQLHelper.ChallongeSQLHelperConnectionString = Environment.GetEnvironmentVariable("dbConnection");
            FrayDbCurrentWeek currentInfo = ChallongeSQLHelper.GetCurrentWeekInfo();
            var postyBoi = Activity.CreateMessageActivity();

            postyBoi.Text = $"Current signup link\n\n[Week {currentInfo.CurrentWeekNum.ToString()} Signup]({currentInfo.SignupUrl})";

            foreach (FrayDbTeamsChannel chan in ChallongeSQLHelper.SqlGetPostChannels())
            {
                try
                {
                    var conversationParameters = new ConversationParameters
                    {
                        IsGroup = true,
                        ChannelData = new TeamsChannelData
                        {
                            Channel = new ChannelInfo(chan.ChannelId),
                        },
                        Activity = (Activity)postyBoi
                    };

                    var connectorClient = new ConnectorClient(new Uri(Environment.GetEnvironmentVariable("ServiceUrl"))
                        , microsoftAppId: Environment.GetEnvironmentVariable("MicrosoftAppId")
                        , microsoftAppPassword: Environment.GetEnvironmentVariable("MicrosoftAppPassword"));
                    MicrosoftAppCredentials.TrustServiceUrl(Environment.GetEnvironmentVariable("ServiceUrl"), DateTime.MaxValue);

                    var response = await connectorClient.Conversations.CreateConversationAsync(conversationParameters);

                    List<FrayDbTeamsUser> frayTagUsers = ChallongeSQLHelper.SqlGetTagUsers(chan.ChannelId);

                    var taggingUsers = Activity.CreateMessageActivity();

                    bool first = true;
                    foreach (FrayDbTeamsUser user in frayTagUsers)
                    {
                        if (!first) { taggingUsers.Text += ", "; }
                        taggingUsers.AddMentionToText(new ChannelAccount(user.UserId, user.UserName), MentionTextLocation.AppendText);
                        first = false;
                    }

                    if (string.IsNullOrEmpty(taggingUsers.Text)) { taggingUsers.Text = "All dressed up and no one to tag"; }

                    await connectorClient.Conversations.ReplyToActivityWithRetriesAsync(response.Id, response.ActivityId, (Activity)taggingUsers);

                    ChallongeSQLHelper.SetCurrentConversation(response.Id);
                }
                catch(Exception ex)
                {
                    //figure out what to do with this
                }
            }

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
