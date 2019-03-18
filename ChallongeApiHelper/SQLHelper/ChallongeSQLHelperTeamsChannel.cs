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
        public static FrayDbTeamsChannel SqlSaveChannel(FrayDbTeamsChannel channel)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ChallongeSQLHelperConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.DB_TEAMS_CHANNEL
                (
                    TEAM_ID,
                    CHANNEL_ID,
                    CHANNEL_NAME,
                    IS_POST
                )
                VALUES
                (
                    @TeamId,
                    @ChannelId,
                    @ChannelName,
                    @IsPost
                )", sqlConnection);

                cmd.Parameters.AddWithValue("@TeamId", channel.TeamId);
                cmd.Parameters.AddWithValue("@ChannelId", channel.ChannelId);
                cmd.Parameters.AddWithValue("@ChannelName", channel.ChannelName);
                cmd.Parameters.AddWithValue("@IsPost", channel.IsPost);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return channel;
        }
    }
}
