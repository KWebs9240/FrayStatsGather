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
        public static FrayDbCurrentWeek GetCurrentWeekInfo()
        {
            var rtnItem = new FrayDbCurrentWeek();

            using (SqlConnection sqlConnection = new SqlConnection(ChallongeSQLHelperConnectionString))
            {
                SqlCommand cmd = new SqlCommand($@"SELECT * FROM DB_CURRENT_WEEK", sqlConnection);
                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rtnItem.CurrentWeekNum = int.Parse(reader["CURRENT_WEEK_NUM"].ToString());
                        rtnItem.SignupUrl = reader["SIGNUP_URL"].ToString();
                        rtnItem.ConversationId = reader["CONVERSATION_ID"].ToString();
                        rtnItem.TournamentId = reader["TOURNAMENT_ID"] == DBNull.Value ? null : (int?)int.Parse(reader["TOURNAMENT_ID"].ToString());
                    }
                }
            }

            return rtnItem;
        }

        public static bool SetCurrentSignupUrl(string newUrl)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ChallongeSQLHelperConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE dbo.DB_CURRENT_WEEK SET
                SIGNUP_URL = @newUrl
                ", sqlConnection);

                cmd.Parameters.AddWithValue("@newUrl", newUrl);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public static bool SetCurrentConversation(string newConversation)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ChallongeSQLHelperConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE dbo.DB_CURRENT_WEEK SET
                CONVERSATION_ID = @newConversation
                ", sqlConnection);

                cmd.Parameters.AddWithValue("@newConversation", newConversation);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public static bool SetCurrentTournamnetId(int newTournyId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ChallongeSQLHelperConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE dbo.DB_CURRENT_WEEK SET
                TOURNAMENT_ID = @newTournyId
                ", sqlConnection);

                cmd.Parameters.AddWithValue("@newTournyId", newTournyId);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public static bool SetCurrentSignupUrlAndConversation(string newUrl, string newConversation)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ChallongeSQLHelperConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE dbo.DB_CURRENT_WEEK SET
                SIGNUP_URL = @newUrl,
                CONVERSATION_ID = @newConversation
                ", sqlConnection);

                cmd.Parameters.AddWithValue("@newUrl", newUrl);
                cmd.Parameters.AddWithValue("@newConversation", newConversation);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return true;
        }

        public static bool ProgressToWeekNum(int weekNum)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ChallongeSQLHelperConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE dbo.DB_CURRENT_WEEK SET
                CURRENT_WEEK_NUM = @weekNum,
                SIGNUP_URL = NULL
                ", sqlConnection);

                cmd.Parameters.AddWithValue("@weekNum", weekNum);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return true;
        }
    }
}
