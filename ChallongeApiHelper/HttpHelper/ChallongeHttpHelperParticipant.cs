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
        public static List<ParticipantRetrieval> GetTournamentParticipants(int tournamentId)
        {
            var participantResponse = BasicGet($"tournaments/{tournamentId}/participants.json");

            return JsonConvert.DeserializeObject<List<ParticipantHolderGarbage>>(participantResponse).Select(x => x.participant).ToList();
        }
    }
}
