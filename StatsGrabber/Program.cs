using ChallongeApiHelper;
using ChallongeEntities.Tournament;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            var getResponse = ChallongeHttpHelper.BasicGet("tournaments.json");

            List<TournamentHolderGarbage> tournamentList = JsonConvert.DeserializeObject<List<TournamentHolderGarbage>>(getResponse);

            int tournamentId = tournamentList.First().tournament.id;
            int tournId2 = tournamentList[1].tournament.id;

            var matchGetResponse = ChallongeHttpHelper.BasicGet($"tournaments/{tournamentId}/matches.json");
            var matchGetResponse2 = ChallongeHttpHelper.BasicGet($"tournaments/{tournId2}/matches.json");
        }
    }
}
