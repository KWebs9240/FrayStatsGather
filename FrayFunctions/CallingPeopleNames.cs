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
    public static class CallingPeopleNames
    {
        [FunctionName("CallingPeopleNames")]
        public static async void Run([TimerTrigger("0 * * * * *"), Disable()]TimerInfo myTimer, ILogger log)
        {   
            var postyBoi = Activity.CreateMessageActivity();

            postyBoi.Text = "Just testing a thing.  Nothing to see here";

            postyBoi.AddMentionToText(new ChannelAccount("29:1NBWvrWJAn7xAH_Uk1xem14_YEmOBccFlXSU_I8gnsPpzJlLuzNC39L5_GIViKymEIxnumn0asG4UlDx9ilUksA", "Master Commander"), MentionTextLocation.AppendText);

            try
            {
                var conversationParameters = new ConversationParameters
                {
                    IsGroup = true,
                    ChannelData = new TeamsChannelData
                    {
                        Channel = new ChannelInfo("19:769656a1a962436a803926a2020dd5c7@thread.skype"),
                    },
                    Activity = (Activity)postyBoi
                };

                var connectorClient = new ConnectorClient(new Uri(Environment.GetEnvironmentVariable("ServiceUrl"))
                    , microsoftAppId: Environment.GetEnvironmentVariable("MicrosoftAppId")
                    , microsoftAppPassword: Environment.GetEnvironmentVariable("MicrosoftAppPassword"));
                MicrosoftAppCredentials.TrustServiceUrl(Environment.GetEnvironmentVariable("ServiceUrl"), DateTime.MaxValue);

                var response = await connectorClient.Conversations.ReplyToActivityWithRetriesAsync("19:769656a1a962436a803926a2020dd5c7@thread.skype;messageid=1559686372455", "NotActuallyImportant", (Activity)postyBoi);

                //var response = await connectorClient.Conversations.CreateConversationAsync(conversationParameters);
            }
            catch(Exception ex)
            {
                //figure out what to do with this
            }

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
