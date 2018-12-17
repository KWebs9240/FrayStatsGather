﻿using ChallongeEntities.Tournament;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallongeApiHelper
{
    public static partial class ChallongeHttpHelper
    {
        public static List<TournamentHolderGarbage> GetAllTournaments()
        {
            var getResponse = ChallongeHttpHelper.BasicGet("tournaments.json");

            return JsonConvert.DeserializeObject<List<TournamentHolderGarbage>>(getResponse);
        }
    }
}
