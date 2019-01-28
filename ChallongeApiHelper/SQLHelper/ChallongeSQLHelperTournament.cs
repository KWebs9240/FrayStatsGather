using ChallongeEntities;
using FrayStatsDbEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.DB_TOURNAMENT
                (
                    TOURNAMENT_ID,
                    TOURNAMENT_NAME,
                    TOURNAMENT_DT
                )
                VALUES
                (
                    @TournamentId,
                    @TournamentName,
                    @TournamentDt
                )", sqlConnection);

                cmd.Parameters.AddWithValue("@TournamentId", tournament.TournamentId);
                cmd.Parameters.AddWithValue("@TournamentName", tournament.TournamentName);
                cmd.Parameters.AddWithValue("@TournamentDt", tournament.TournamentDt);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return tournament;
        }
    }
}
