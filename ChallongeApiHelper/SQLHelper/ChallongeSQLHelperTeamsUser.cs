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
        public static FrayDbTeamsUser SqlGetSingleUser(string id)
        {
            FrayDbTeamsUser rtnItem = null;

            using (SqlConnection sqlConnection = new SqlConnection(ChallongeSQLHelperConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM dbo.DB_TEAMS_USER WHERE USER_ID = @UserId", sqlConnection);

                cmd.Parameters.AddWithValue("@UserId", id);

                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rtnItem = new FrayDbTeamsUser();

                        rtnItem.UserId = reader["USER_ID"].ToString();
                        rtnItem.UserName = reader["USER_NAME"].ToString();
                        rtnItem.IsTag = bool.Parse(reader["IS_TAG"].ToString());
                    }
                }
            }

            return rtnItem;
        }

        public static FrayDbTeamsUser SqlInsertTeamsUser(FrayDbTeamsUser user)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ChallongeSQLHelperConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.DB_TEAMS_USER
                (
                    USER_ID,
                    USER_NAME,
                    IS_TAG
                )
                VALUES
                (
                    @UserId,
                    @UserName,
                    @IsTag
                )", sqlConnection);

                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@IsTag", user.IsTag);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return user;
        }

        public static FrayDbTeamsUser SqlUpdateTeamsUser(string id, FrayDbTeamsUser user)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ChallongeSQLHelperConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE dbo.DB_TEAMS_USER SET
                USER_ID = @UserId,
                USER_NAME = @UserName,
                IS_TAG = @IsTag
                WHERE USER_ID = @OldUserId", sqlConnection);

                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@IsTag", user.IsTag);
                cmd.Parameters.AddWithValue("@OldUserId", id);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return user;
        }
    }
}
