using ChallongeEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallongeApiHelper.HttpHelper
{
    public static partial class ChallongeHttpHelper
    {
        public static List<TournamentRetrieval> GetAllTournaments()
        {
            var getResponse = BasicGet("tournaments.json");

            return JsonConvert.DeserializeObject<List<TournamentHolderGarbage>>(getResponse).Select(x => x.tournament).ToList();
        }
    }
}
