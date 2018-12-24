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
        public static FrayDbMatch SqlSaveMatch(FrayDbMatch match)
        {
            using (SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\LocalTesting;Initial Catalog=FrayData;Integrated Security=true;"))
            {
                SqlCommand cmd = new SqlCommand($@"INSERT INTO dbo.MATCH
                (
                    MATCH_ID,
                    WINNER_ID,
                    LOSER_ID,
                    PLAYER1_ID,
                    PLAYER2_ID,
                    TOURNAMENT_ID
                )
                VALUES
                (
                    {match.MatchId},
                    {match.WinnerId},
                    {match.LoserId},
                    {match.Player1Id},
                    {match.Player2Id},
                    {match.TournamentId}
                )", sqlConnection);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return match;
        }
    }
}
