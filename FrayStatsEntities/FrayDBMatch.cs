using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrayStatsDbEntities
{
    public class FrayDbMatch
    {
        public int MatchId { get; set; }
        public int? WinnerId { get; set; }
        public int? LoserId { get; set; }
        public int? Player1Id { get; set; }
        public int? Player2Id { get; set; }

        public DateTime LastCheckDate { get; set; }
    }
}
