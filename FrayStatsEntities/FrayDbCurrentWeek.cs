using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrayStatsDbEntities
{
    public class FrayDbCurrentWeek
    {
        public int CurrentWeekNum { get; set; }
        public string SignupUrl { get; set; }
        public string ConversationId { get; set; }
        public int? TournamentId { get; set; }
    }
}
