using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrayStatsDbEntities
{
    public class FrayDbMatch
    {
        public long MatchId { get; set; }
        public int? WinnerId { get; set; }
        public int? LoserId { get; set; }
        public int? Player1Id { get; set; }
        public int? Player2Id { get; set; }
        public int MatchRank { get; set; } //Whether the match was finals, semis... 1 being finals, 2 being semis... 100 being 3rd place match
        public long TournamentId { get; set; }
    }
}
