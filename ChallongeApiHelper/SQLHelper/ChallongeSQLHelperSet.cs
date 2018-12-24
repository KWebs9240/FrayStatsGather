using ChallongeEntities;
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
        public static FrayDbSet SqlSaveSet(FrayDbSet set)
        {
            using (SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\LocalTesting;Initial Catalog=FrayData;Integrated Security=true;"))
            {
                SqlCommand cmd = new SqlCommand($@"INSERT INTO dbo.MATCH
                (
                    MATCH_ID,
                    SET_NO,
                    PLAYER1_SCORE,
                    PLAYER2_SCORE
                )
                VALUES
                (
                    {set.MatchId},
                    {set.SetNo},
                    {set.Player1Score},
                    {set.Player2Score}
                )", sqlConnection);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return set;
        }
    }
}
