using ChallongeApiHelper;
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
            string thing = ConfigurationManager.AppSettings["ApiUsername"];
            ChallongeHttpHelper.setAuthorizationHeader(ConfigurationManager.AppSettings["ApiUsername"], ConfigurationManager.AppSettings["ApiPassword"]);

            List<TournamentRetrieval> tournamentList = ChallongeHttpHelper.GetAllTournaments();

            Dictionary<int, List<MatchRetrieval>> tournyIdMatchDict = new Dictionary<int, List<MatchRetrieval>>();
            Dictionary<int, List<ParticipantRetrieval>> tournyIdParticipantDict = new Dictionary<int, List<ParticipantRetrieval>>();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach(TournamentRetrieval tournament in tournamentList)
            {
                tournyIdMatchDict.Add(tournament.id, ChallongeHttpHelper.GetTournamentMatches(tournament.id));
                tournyIdParticipantDict.Add(tournament.id, ChallongeHttpHelper.GetTournamentParticipants(tournament.id));
            }

            sw.Stop();
        }
    }
}
