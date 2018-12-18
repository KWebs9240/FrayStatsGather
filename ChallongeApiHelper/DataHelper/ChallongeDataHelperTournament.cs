using ChallongeApiHelper.HttpHelper;
using ChallongeEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallongeApiHelper.DataHelper
{
    public static partial class ChallongeDataHelper
    {
        private static List<TournamentRetrieval> _allTournaments = null;

        public static List<TournamentRetrieval> GetAllTournaments()
        {
            if(_allTournaments == null)
            {
                _allTournaments = ChallongeHttpHelper.GetAllTournaments();
            }

            return _allTournaments;
        }
    }
}
