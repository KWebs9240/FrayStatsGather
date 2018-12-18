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
        public static List<MatchRetrieval> GetTournamentMatches(int tournamentId)
        {
            var matchGetResponse = BasicGet($"tournaments/{tournamentId}/matches.json");

            return JsonConvert.DeserializeObject<List<MatchHolderGarbage>>(matchGetResponse).Select(x => x.match).ToList();
        }
    }
}
