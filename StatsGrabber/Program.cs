using ChallongeApiHelper;
using ChallongeApiHelper.DataHelper;
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


        }
    }
}
