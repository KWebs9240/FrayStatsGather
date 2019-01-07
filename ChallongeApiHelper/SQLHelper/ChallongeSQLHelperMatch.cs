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
                SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.MATCH
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
                    @MatchId,
                    @WinnerId,
                    @LoserId,
                    @Player1Id,
                    @Player2Id,
                    @TournamentId
                )", sqlConnection);

                cmd.Parameters.AddWithValue("@MatchId", match.MatchId);
                cmd.Parameters.AddWithValue("@WinnerId", match.WinnerId);
                cmd.Parameters.AddWithValue("@LoserId", match.LoserId);
                cmd.Parameters.AddWithValue("@Player1Id", match.Player1Id);
                cmd.Parameters.AddWithValue("@Player2Id", match.Player2Id);
                cmd.Parameters.AddWithValue("@TournamentId", match.TournamentId);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return match;
        }
    }
}
