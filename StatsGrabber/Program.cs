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
            List<TournamentRetrieval> tournamentList = ChallongeDataHelper.GetAllTournaments();

            TournamentRetrieval tournament = tournamentList.First();
            List<MatchRetrieval> tournamentMatches = ChallongeDataHelper.GetTournamentMatches(tournament.id);
            List<ParticipantRetrieval> tournamentParticipants = ChallongeDataHelper.GetTournamentParticipants(tournament.id);

            FrayDbTournament dbTournament = new FrayDbTournament()
            {
                TournamentName = tournament.name,
                TournamentId = tournament.id,
                TournamentDt = tournament.started_at.Value
            };

            ChallongeSQLHelper.SqlSaveTournament(dbTournament);

            foreach(ParticipantRetrieval retrievedParticipant in tournamentParticipants)
            {
                FrayDbParticipant newParticipant = new FrayDbParticipant()
                {
                    ChallongeUserName = retrievedParticipant.challonge_username
                };

                ChallongeSQLHelper.SqlSaveParticipant(newParticipant);
            }

            List<FrayDbParticipant> allKnownParticipants = ChallongeSQLHelper.SqlGetParticipants();

            foreach (MatchRetrieval retrievedMatch in tournamentMatches)
            {
                FrayDbMatch newMatch = new FrayDbMatch();
                newMatch.MatchId = retrievedMatch.id;

                ParticipantRetrieval player1 = tournamentParticipants.First(x => x.id.Equals(retrievedMatch.player1_id));
                FrayDbParticipant actuallyPlayer1 = allKnownParticipants.First(x => x.ChallongeUserName.Equals(player1.challonge_username));

                ParticipantRetrieval player2 = tournamentParticipants.First(x => x.id.Equals(retrievedMatch.player2_id));
                FrayDbParticipant actuallyPlayer2 = allKnownParticipants.First(x => x.ChallongeUserName.Equals(player2.challonge_username));

                newMatch.Player1Id = Convert.ToInt32(actuallyPlayer1.ParticipantId);
                newMatch.Player2Id = Convert.ToInt32(actuallyPlayer2.ParticipantId);

                if(retrievedMatch.winner_id.Equals(player1.id))
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

                foreach(string set in retrievedMatch.scores_csv.Split(','))
                {
                    currentSetNo++;

                    FrayDbSet newSet = new FrayDbSet();
                    newSet.MatchId = retrievedMatch.id;
                    newSet.SetNo = currentSetNo;
                    var setScore = set.Split('-');

                    newSet.Player1Score = int.Parse(setScore[0]);
                    newSet.Player2Score = int.Parse(setScore[1]);

                    ChallongeSQLHelper.SqlSaveSet(newSet);
                }
            }
        }
    }
}
