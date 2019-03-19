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
        public static FrayDbTeamsChannel SqlGetSingleChannel(string channelId, string teamId)
        {
            FrayDbTeamsChannel rtnItem = null;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM dbo.DB_TEAMS_CHANNEL 
                    WHERE CHANNEL_ID = @ChannelId 
                    AND TEAM_ID = @TeamId", sqlConnection);

                cmd.Parameters.AddWithValue("@ChannelId", channelId);
                cmd.Parameters.AddWithValue("@TeamId", teamId);

                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rtnItem = new FrayDbTeamsChannel();

                        rtnItem.ChannelId = reader["CHANNEL_ID"].ToString();
                        rtnItem.TeamId = reader["TEAM_ID"].ToString();
                        rtnItem.ChannelName = reader["CHANNEL_NAME"].ToString();
                        rtnItem.IsPost = bool.Parse(reader["IS_POST"].ToString());
                    }
                }
            }

            return rtnItem;
        }

        public static List<FrayDbTeamsChannel> SqlGetPostChannels()
        {
            List<FrayDbTeamsChannel> rtnList = new List<FrayDbTeamsChannel>();

            using (SqlConnection sqlConnection = new SqlConnection(ChallongeSQLHelperConnectionString))
            {
                SqlCommand cmd = new SqlCommand($@"SELECT * FROM DB_TEAMS_CHANNEL WHERE IS_POST = 1", sqlConnection);
                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FrayDbTeamsChannel part = new FrayDbTeamsChannel();
                        part.TeamId = reader["TEAM_ID"].ToString();
                        part.ChannelId = reader["CHANNEL_ID"].ToString();
                        part.ChannelName = reader["CHANNEL_NAME"].ToString();
                        part.IsPost = bool.Parse(reader["IS_POST"].ToString());

                        rtnList.Add(part);
                    }
                }
            }

            return rtnList;
        }

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
