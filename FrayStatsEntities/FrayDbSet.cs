﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrayStatsEntities
{
    public class FrayDbSet
    {
        public int MatchId { get; set; }
        public int SetNo { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }

        public DateTime LastCheckDate { get; set; }
    }
}
