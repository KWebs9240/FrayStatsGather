using ChallongeApiHelper.HttpHelper;
using ChallongeEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallongeApiHelper.DataHelper
{
    public static partial class ChallongeDataHelper
    {
        private static Dictionary<int, List<MatchRetrieval>> _tournamentMatches = new Dictionary<int, List<MatchRetrieval>>();

        public static List<MatchRetrieval> GetTournamentMatches(int tournamentId)
        {
            if(!_tournamentMatches.ContainsKey(tournamentId))
            {
                _tournamentMatches.Add(tournamentId, ChallongeHttpHelper.GetTournamentMatches(tournamentId));
            }

            return _tournamentMatches[tournamentId];
        }
    }
}
