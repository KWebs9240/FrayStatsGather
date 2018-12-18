using ChallongeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreStatsGather
{
    public static class ChallongeStaticHelpers
    {
        public static ParticipantRetrieval DetermineTournamentWinner (List<MatchRetrieval> tournamentMatchList, List<ParticipantRetrieval> participantList)
        {
            int? finalMatchWinner = tournamentMatchList.OrderByDescending(x => x.round).First().winner_id;

            if(finalMatchWinner.HasValue) { return participantList.FirstOrDefault(x => x.id.Equals(finalMatchWinner.Value)); }

            return null;
        }
    }
}
