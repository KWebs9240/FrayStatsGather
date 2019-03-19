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
        public static FrayDbTeamsTeam SqlGetSingleTeam(string id)
        {
            FrayDbTeamsTeam rtnItem = null;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM dbo.DB_TEAMS_TEAM WHERE TEAM_ID = @TeamId", sqlConnection);

                cmd.Parameters.AddWithValue("@TeamId", id);

                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rtnItem = new FrayDbTeamsTeam();

                        rtnItem.TeamId = reader["TEAM_ID"].ToString();
                        rtnItem.TeamName = reader["TEAM_NAME"].ToString();
                    }
                }
            }

            return rtnItem;
        }

        public static FrayDbTeamsTeam SqlSaveTeam(FrayDbTeamsTeam team)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ChallongeSQLHelperConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.DB_TEAMS_TEAM
                (
                    TEAM_ID,
                    TEAM_NAME
                )
                VALUES
                (
                    @TeamId,
                    @TeamName
                )", sqlConnection);

                cmd.Parameters.AddWithValue("@TeamId", team.TeamId);
                cmd.Parameters.AddWithValue("@TeamName", team.TeamName);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return team;
        }
    }
}
