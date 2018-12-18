using ChallongeApiHelper;
using ChallongeApiHelper.DataHelper;
using ChallongeEntities;
using CoreStatsGather;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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

            Dictionary<int, List<MatchRetrieval>> tournyIdMatchDict = new Dictionary<int, List<MatchRetrieval>>();
            Dictionary<int, List<ParticipantRetrieval>> tournyIdParticipantDict = new Dictionary<int, List<ParticipantRetrieval>>();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach(TournamentRetrieval tournament in tournamentList)
            {
                tournyIdMatchDict.Add(tournament.id, ChallongeDataHelper.GetTournamentMatches(tournament.id));
                tournyIdParticipantDict.Add(tournament.id, ChallongeDataHelper.GetTournamentParticipants(tournament.id));
            }

            sw.Stop();
        }
    }
}
