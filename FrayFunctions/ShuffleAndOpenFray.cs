using System;
using System.Collections.Generic;
using ChallongeApiHelper.HttpHelper;
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
    public static class ShuffleAndOpenFray
    {
        [FunctionName("ShuffleAndOpenFray")]
        public static async void Run([TimerTrigger("0 36 14 * * 5"), Disable()]TimerInfo myTimer, ILogger log)
        {
            ChallongeSQLHelper.ChallongeSQLHelperConnectionString = Environment.GetEnvironmentVariable("dbConnection");
            ChallongeApiHelper.HttpHelper.ChallongeHttpHelper.setAuthorizationHeader(Environment.GetEnvironmentVariable("ApiUsername"), Environment.GetEnvironmentVariable("ApiPassword"));

            FrayDbCurrentWeek currentWeek = ChallongeSQLHelper.GetCurrentWeekInfo();

            if(!currentWeek.TournamentId.HasValue)
            {
                return;
            }

            ChallongeHttpHelper.ShuffleSeeds(currentWeek.TournamentId.Value);
            ChallongeHttpHelper.OpenPredictionsTournament(currentWeek.TournamentId.Value);

            var postyBoi = Activity.CreateMessageActivity();

            postyBoi.Text = "Sign-ups are closed. Predictions are open";

            try
            {
                var connectorClient = new ConnectorClient(new Uri(Environment.GetEnvironmentVariable("ServiceUrl"))
                    , microsoftAppId: Environment.GetEnvironmentVariable("MicrosoftAppId")
                    , microsoftAppPassword: Environment.GetEnvironmentVariable("MicrosoftAppPassword"));
                MicrosoftAppCredentials.TrustServiceUrl(Environment.GetEnvironmentVariable("ServiceUrl"), DateTime.MaxValue);

                var response = await connectorClient.Conversations.ReplyToActivityWithRetriesAsync(currentWeek.ConversationId, "NotActuallyImportant", (Activity)postyBoi);
            }
            catch (Exception ex)
            {
                //figure out what to do with this
            }

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
