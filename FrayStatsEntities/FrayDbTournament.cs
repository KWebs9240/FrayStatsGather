using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrayStatsDbEntities
{
    public class FrayDbTournament
    {
        public int TournamentId { get; set; }
        public string TournamentName { get; set; }
        public DateTime TournamentDt { get; set; }
    }
}
