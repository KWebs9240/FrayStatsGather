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
        public static List<FrayDbParticipant> SqlGetParticipants()
        {
            List<FrayDbParticipant> rtnList = new List<FrayDbParticipant>();

            using (SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\LocalTesting;Initial Catalog=FrayData;Integrated Security=true;"))
            {
                SqlCommand cmd = new SqlCommand($@"SELECT * FROM PARTICIPANT", sqlConnection);
                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FrayDbParticipant part = new FrayDbParticipant();
                        part.ParticipantId = long.Parse(reader["PARTICIPANT_ID"].ToString());
                        part.ChallongeUserName = reader["CHALLONGE_USERNAME"].ToString();
                        part.ParticipantName = reader["NAME"].ToString();

                        rtnList.Add(part);
                    }
                }
            }

            return rtnList;
        }

        public static FrayDbParticipant SqlSaveParticipant(FrayDbParticipant participant)
        {
            using (SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\LocalTesting;Initial Catalog=FrayData;Integrated Security=true;"))
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO dbo.MATCH
                (
                    CHALLONGE_USERNAME,
                    NAME
                )
                VALUES
                (
                    @ChallongeUserName,
                    @ParticipantName
                )", sqlConnection);

                cmd.Parameters.AddWithValue("@ChallongeUserName", participant.ChallongeUserName);
                cmd.Parameters.AddWithValue("@ParticipantName", participant.ParticipantName);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return participant;
        }
    }
}
