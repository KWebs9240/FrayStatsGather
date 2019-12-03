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

        public static List<TournamentRetrieval> GetRecentTournaments()
        {
            var getResponse = BasicGet($"tournaments.json?created_after={DateTime.UtcNow.AddMonths(-1).ToString("yyyy-MM-dd")}");

            return JsonConvert.DeserializeObject<List<TournamentHolderGarbage>>(getResponse).Select(x => x.tournament).ToList();
        }

        public static TournamentRetrieval PostNewTournament(TournamentCreation newTournament)
        {
            var postResponse = Post("tournaments.json", newTournament);

            return JsonConvert.DeserializeObject<TournamentHolderGarbage>(postResponse).tournament;
        }

        public static string OpenPredictionsTournament(int tournamentId)
        {
            var postResponse = Post($"tournaments/{tournamentId}/open_for_predictions.json", string.Empty);

            return postResponse;
        }

        public static string StartTournament(int tournamentId)
        {
            var postResponse = Post($"tournaments/{tournamentId}/start.json", string.Empty);

            return postResponse;
        }
    }
}
