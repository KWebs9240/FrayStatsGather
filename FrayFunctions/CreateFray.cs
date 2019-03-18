using System;
using System.Collections.Generic;
using ChallongeApiHelper.SQLHelper;
using ChallongeEntities;
using FrayStatsDbEntities;
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

            ChallongeApiHelper.SQLHelper.ChallongeSQLHelper.ChallongeSQLHelperConnectionString = Environment.GetEnvironmentVariable("dbConnection");

            FrayDbCurrentWeek currentWeekInfo = ChallongeApiHelper.SQLHelper.ChallongeSQLHelper.GetCurrentWeekInfo();

            TournamentCreation tournamentToCreate = new TournamentCreation()
            {
                name = $"QDAL Friday Fray - Week {currentWeekInfo.CurrentWeekNum.ToString()}",
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

            ChallongeSQLHelper.SetCurrentSignupUrl(createdTournamnet.sign_up_url);

            log.LogInformation("Weekly tournament scheduled succesfully");
        }
    }
}
