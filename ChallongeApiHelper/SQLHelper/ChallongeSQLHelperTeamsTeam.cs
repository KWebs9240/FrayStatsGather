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
