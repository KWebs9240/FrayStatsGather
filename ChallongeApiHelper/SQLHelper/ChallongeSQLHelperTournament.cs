﻿using ChallongeEntities;
using FrayStatsDbEntities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallongeApiHelper.SQLHelper
{
    public static partial class ChallongeSQLHelper
    {
        public static FrayDbTournament SqlSaveTournament(FrayDbTournament tournament)
        {
            using (SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\LocalTesting;Initial Catalog=FrayData;Integrated Security=true;"))
            {
                SqlCommand cmd = new SqlCommand($@"INSERT INTO dbo.TOURNAMENT
                (
                    TOURNAMENT_ID,
                    TOURNAMENT_NAME,
                    TOURNAMENT_DT
                )
                VALUES
                (
                    {tournament.TournamentId}, -- TOURNAMENT_ID - numeric(18, 0)
                    N'{tournament.TournamentName}',   -- TOURNAMENT_NAME - nvarchar(50)
                    '{tournament.TournamentDt.ToString()}'
                )", sqlConnection);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return tournament;
        }
    }
}
