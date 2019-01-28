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
        public static FrayDbMatch SqlSaveMatch(FrayDbMatch match)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.DB_MATCH
                (
                    MATCH_ID,
                    WINNER_ID,
                    LOSER_ID,
                    PLAYER1_ID,
                    PLAYER2_ID,
                    TOURNAMENT_ID,
                    MATCH_RANK
                )
                VALUES
                (
                    @MatchId,
                    @WinnerId,
                    @LoserId,
                    @Player1Id,
                    @Player2Id,
                    @TournamentId,
                    @MatchRank
                )", sqlConnection);

                cmd.Parameters.AddWithValue("@MatchId", match.MatchId);
                cmd.Parameters.AddWithValue("@WinnerId", match.WinnerId);
                cmd.Parameters.AddWithValue("@LoserId", match.LoserId);
                cmd.Parameters.AddWithValue("@Player1Id", match.Player1Id);
                cmd.Parameters.AddWithValue("@Player2Id", match.Player2Id);
                cmd.Parameters.AddWithValue("@TournamentId", match.TournamentId);
                cmd.Parameters.AddWithValue("@MatchRank", match.MatchRank);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return match;
        }
    }
}
