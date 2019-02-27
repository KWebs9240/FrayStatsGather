using System;
using ChallongeEntities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FrayFunctions
{
    public static class CreateFray
    {
        [FunctionName("CreateFray")]
        public static void Run([TimerTrigger("0 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            //Doing this way for now since the new version of the functions doesn't seem to play nice with the configuration manager
            ChallongeApiHelper.HttpHelper.ChallongeHttpHelper.setAuthorizationHeader(Environment.GetEnvironmentVariable("ApiUsername"), Environment.GetEnvironmentVariable("ApiPassword"));

            TournamentCreation tournamentToCreate = new TournamentCreation()
            {
                //name = $"QDAL Friday Fray - Week {weekNum.ToString()}",
                name = "Kyle Test",
                url = Guid.NewGuid().ToString().Replace("-", ""),
                tournament_type = TournamentConstants.TournamentType.SingleElimination,
                open_signup = true,
                hold_third_place_match = false,
                pts_for_game_win = 0.0m,
                pts_for_game_tie = 0.0m,
                pts_for_match_win = 1.0m,
                pts_for_match_tie = 0.5m,
                pts_for_bye = 1.0m,
                swiss_rounds = 0,
                @private = false,
                ranked_by = TournamentConstants.RankedBy.MatchWins,
                show_rounds = true,
                hide_forum = false,
                sequential_pairings = false,
                rr_pts_for_game_win = 0.0m,
                rr_pts_for_game_tie = 0.0m,
                rr_pts_for_match_win = 0.0m,
                rr_pts_for_match_tie = 0.0m,
                prediction_method = TournamentConstants.PredictionMethod.Exponential,
                game_id = TournamentConstants.GameId.PingPong
            };

            var createdTournamnet = ChallongeApiHelper.HttpHelper.ChallongeHttpHelper.PostNewTournament(tournamentToCreate);

            string signUpUrl = createdTournamnet.sign_up_url;
            log.LogInformation($"Signup url is {signUpUrl}");
            //Email whoever cares
            //MailMessage mail = new MailMessage
            //{
            //    From = new MailAddress("weeklypingpong@gmail.com")
            //};

            //string[] emailRecipients = ConfigurationManager.AppSettings["EmailList"].Split(';');

            //foreach (var recipient in emailRecipients)
            //{
            //    mail.To.Add(recipient);
            //}

            //SmtpClient client = new SmtpClient("smtp.gmail.com")
            //{
            //    Port = 587,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    EnableSsl = true
            //};
            //client.Credentials = new System.Net.NetworkCredential("thatoneemail", ConfigurationManager.AppSettings["EmailPassword"]);

            //mail.Subject = "Ping Pong Email";
            //mail.IsBodyHtml = true;
            //mail.Body = $"Tournament Created - QDAL Friday Fray - Week {weekNum.ToString()} <br /> Signup Link: {signUpUrl}";

            //client.Send(mail);

            log.LogInformation("Weekly tournament scheduler succesfully, sent emails!");
        }
    }
}
