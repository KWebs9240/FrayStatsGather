using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrayStatsDbEntities
{
    public class FrayDbTeamsChannel
    {
        public string TeamId { get; set; }
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public bool IsPost { get; set; }
    }
}
