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

        public static List<ParticipantRetrieval> ShuffleSeeds(int tournamentId)
        {
            var postResponse = Post($"tournaments/{tournamentId.ToString()}/participants/randomize.json", string.Empty);

            return JsonConvert.DeserializeObject<List<ParticipantHolderGarbage>>(postResponse).Select(x => x.participant).ToList();
        }
}
}
