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
        public static FrayDbParticipant SqlSaveParticipant(FrayDbParticipant participant)
        {
            using (SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\LocalTesting;Initial Catalog=FrayData;Integrated Security=true;"))
            {
                SqlCommand cmd = new SqlCommand($@"INSERT INTO dbo.MATCH
                (
                    PARTICIPANT_ID,
                    CHALLONGE_USERNAME,
                    NAME
                )
                VALUES
                (
                    {participant.ParticipantId},
                    N'{participant.ChallongeUserName}',
                    N'{participant.ParticipantName}'
                )", sqlConnection);

                sqlConnection.Open();

                cmd.ExecuteNonQuery();
            }

            return participant;
        }
    }
}
