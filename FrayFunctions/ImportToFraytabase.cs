using System;
using System.Collections.Generic;
using System.Linq;
using ChallongeApiHelper.DataHelper;
using ChallongeApiHelper.SQLHelper;
using ChallongeEntities;
using FrayStatsDbEntities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FrayFunctions
{
    public static class ImportToFraytabase
    {
        [FunctionName("ImportToFraytabase")]
        public static void Run([TimerTrigger("0 0 11 * * 1")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            //Doing this way for now since the new version of the functions doesn't seem to play nice with the configuration manager
            ChallongeApiHelper.HttpHelper.ChallongeHttpHelper.setAuthorizationHeader(Environment.GetEnvironmentVariable("ApiUsername"), Environment.GetEnvironmentVariable("ApiPassword"));

            ChallongeSQLHelper.ChallongeSQLHelperConnectionString = Environment.GetEnvironmentVariable("dbConnection");

            List<TournamentRetrieval> tournamentList = ChallongeApiHelper.HttpHelper.ChallongeHttpHelper.GetRecentTournaments();

            FrayDbCurrentWeek currentWeekInfo = ChallongeSQLHelper.GetCurrentWeekInfo();

            tournamentList = tournamentList
                .Where(x => x.started_at.HasValue)
                .Where(x => x.name.Contains($"Week {currentWeekInfo.CurrentWeekNum.ToString()}"))
                .ToList();

            foreach (TournamentRetrieval tournament in tournamentList)
            {
                Console.WriteLine($"Currently importing {tournament.name}");

                List<MatchRetrieval> tournamentMatches = ChallongeDataHelper.GetTournamentMatches(tournament.id);
                List<ParticipantRetrieval> tournamentParticipants = ChallongeDataHelper.GetTournamentParticipants(tournament.id);

                FrayDbTournament dbTournament = new FrayDbTournament()
                {
                    TournamentName = tournament.name,
                    TournamentId = tournament.id,
                    TournamentDt = tournament.started_at.Value
                };

                ChallongeSQLHelper.SqlSaveTournament(dbTournament);

                List<FrayDbParticipant> existingParticipants = ChallongeSQLHelper.SqlGetParticipants();
                HashSet<string> knownParticipants = new HashSet<string>(existingParticipants.Select(x => x.ChallongeUserName));

                foreach (ParticipantRetrieval retrievedParticipant in tournamentParticipants)
                {
                    if (string.IsNullOrEmpty(retrievedParticipant.challonge_username)) { retrievedParticipant.challonge_username = "Unknown"; }

                    if (!knownParticipants.Contains(retrievedParticipant.challonge_username))
                    {
                        FrayDbParticipant newParticipant = new FrayDbParticipant()
                        {
                            ChallongeUserName = retrievedParticipant.challonge_username
                        };

                        ChallongeSQLHelper.SqlSaveParticipant(newParticipant);
                    }
                }

                List<FrayDbParticipant> allKnownParticipants = ChallongeSQLHelper.SqlGetParticipants();

                int maxRank = tournamentMatches.Select(x => x.round).Max();

                foreach (MatchRetrieval retrievedMatch in tournamentMatches.Where(x => x.winner_id.HasValue))
                {
                    FrayDbMatch newMatch = new FrayDbMatch();
                    newMatch.MatchId = retrievedMatch.id;
                    newMatch.MatchRank = retrievedMatch.round.Equals(0)
                        ? 100
                        : maxRank + 1 - retrievedMatch.round;

                    ParticipantRetrieval player1 = tournamentParticipants.First(x => x.id.Equals(retrievedMatch.player1_id));
                    FrayDbParticipant actuallyPlayer1 = allKnownParticipants.First(x => x.ChallongeUserName.Equals(player1.challonge_username));

                    ParticipantRetrieval player2 = tournamentParticipants.First(x => x.id.Equals(retrievedMatch.player2_id));
                    FrayDbParticipant actuallyPlayer2 = allKnownParticipants.First(x => x.ChallongeUserName.Equals(player2.challonge_username));

                    newMatch.Player1Id = Convert.ToInt32(actuallyPlayer1.ParticipantId);
                    newMatch.Player2Id = Convert.ToInt32(actuallyPlayer2.ParticipantId);

                    if (retrievedMatch.winner_id.Equals(player1.id))
                    {
                        newMatch.WinnerId = Convert.ToInt32(actuallyPlayer1.ParticipantId);
                        newMatch.LoserId = Convert.ToInt32(actuallyPlayer2.ParticipantId);
                    }
                    else
                    {
                        newMatch.WinnerId = Convert.ToInt32(actuallyPlayer2.ParticipantId);
                        newMatch.LoserId = Convert.ToInt32(actuallyPlayer1.ParticipantId);
                    }

                    newMatch.TournamentId = tournament.id;

                    ChallongeSQLHelper.SqlSaveMatch(newMatch);

                    int currentSetNo = 0;

                    foreach (string set in retrievedMatch.scores_csv.Split(','))
                    {
                        currentSetNo++;

                        FrayDbSet newSet = new FrayDbSet();
                        newSet.MatchId = retrievedMatch.id;
                        newSet.SetNo = currentSetNo;

                        if (set.IndexOf('-') == 0)
                        {
                            int splitMarker = set.IndexOf('-', 1);

                            newSet.Player1Score = int.Parse(set.Substring(0, splitMarker));
                            newSet.Player2Score = int.Parse(set.Substring(splitMarker + 1));
                        }
                        else
                        {
                            int splitMarker = set.IndexOf('-');

                            newSet.Player1Score = int.Parse(set.Substring(0, splitMarker));
                            newSet.Player2Score = int.Parse(set.Substring(splitMarker + 1));
                        }

                        ChallongeSQLHelper.SqlSaveSet(newSet);
                    }
                }
            }

            ChallongeSQLHelper.ProgressToWeekNum(currentWeekInfo.CurrentWeekNum + 1);
        }
    }
}
