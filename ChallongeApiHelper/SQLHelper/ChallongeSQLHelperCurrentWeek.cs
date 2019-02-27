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
        public static int GetCurrentWeek()
        {
            int rtnItem = -1;

            using (SqlConnection sqlConnection = new SqlConnection(ChallongeSQLHelperConnectionString))
            {
                SqlCommand cmd = new SqlCommand($@"SELECT * FROM DB_CURRENT_WEEK", sqlConnection);
                sqlConnection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rtnItem = int.Parse(reader["CURRENT_WEEK_NUM"].ToString());
                    }
                }
            }

            return rtnItem;
        }
    }
}
