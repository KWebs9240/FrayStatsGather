using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrayStatsEntities
{
    public class FrayDbParticipant
    {
        public int ParticipantId { get; set; }
        public string ChallongeUserName { get; set; }

        public DateTime LastCheckDate { get; set; }
    }
}
