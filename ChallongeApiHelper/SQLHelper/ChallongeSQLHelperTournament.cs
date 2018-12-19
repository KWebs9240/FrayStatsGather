using ChallongeEntities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallongeApiHelper.SQLHelper
{
    public static class ChallongeSQLHelperTournament
    {
        public static TournamentRetrieval SqlSaveTournament(TournamentRetrieval tournament)
        {
            using (SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\LocalTesting;Initial Catalog=FrayData;Integrated Security=true;"))
            {
                SqlCommand cmd = new SqlCommand($@"INSERT INTO dbo.TOURNAMENT
                (
                    TOURNAMENT_ID,
                    TOURNAMENT_NAME
                )
                VALUES
                (
                    {tournament.id}, -- TOURNAMENT_ID - numeric(18, 0)
                    N'{tournament.name}'   -- TOURNAMENT_NAME - nvarchar(50)
                )", sqlConnection);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return tournament;
        }
    }
}
