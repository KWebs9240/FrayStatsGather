using System;
using ChallongeApiHelper.SQLHelper;
using FrayStatsDbEntities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Connector.Teams.Models;
using Microsoft.Bot.Schema;
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

            postyBoi.Text = $"Current signup link\n\n[Drop it like it's hot]({currentInfo.SignupUrl})";

            foreach (FrayDbTeamsChannel chan in ChallongeSQLHelper.SqlGetPostChannels())
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
            }

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
