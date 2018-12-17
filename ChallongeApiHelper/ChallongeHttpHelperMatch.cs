using ChallongeEntities.Match;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallongeApiHelper
{
    public static partial class ChallongeHttpHelper
    {
        public static List<MatchHolderGarbage> GetTournamentMatches(int tournamentId)
        {
            var matchGetResponse = BasicGet($"tournaments/{tournamentId}/matches.json");

            return JsonConvert.DeserializeObject<List<MatchHolderGarbage>>(matchGetResponse);
        }
    }
}
