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
        }
    }
}
