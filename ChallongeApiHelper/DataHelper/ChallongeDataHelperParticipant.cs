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
        private static Dictionary<int, List<ParticipantRetrieval>> _tournamentParticipants = new Dictionary<int, List<ParticipantRetrieval>>();

        public static List<ParticipantRetrieval> GetTournamentParticipants(int tournamentId)
        {
            if(!_tournamentParticipants.ContainsKey(tournamentId))
            {
                _tournamentParticipants.Add(tournamentId, ChallongeHttpHelper.GetTournamentParticipants(tournamentId));
            }

            return _tournamentParticipants[tournamentId];
        }
    }
}
