using ChallongeApiHelper;
using ChallongeApiHelper.DataHelper;
using ChallongeApiHelper.SQLHelper;
using ChallongeEntities;
using CoreStatsGather;
using FrayStatsDbEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            ChallongeSQLHelper.ChallongeSQLHelperConnectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

            List<TournamentRetrieval> tournamentList = ChallongeDataHelper.GetAllTournaments();

            tournamentList = tournamentList.Where(x => x.started_at.HasValue).ToList();

            tournamentList = tournamentList.Where(x => x.name.Contains("Week 52")).ToList();

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
        }
    }
}
