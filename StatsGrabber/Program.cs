using ChallongeApiHelper;
using ChallongeApiHelper.DataHelper;
using ChallongeEntities;
using CoreStatsGather;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            TournamentRetrieval test = new TournamentRetrieval()
            {
                id = 1,
                name = "Test Save Code"
            };

            ChallongeApiHelper.SQLHelper.ChallongeSQLHelperTournament.SqlSaveTournament(test);

            //using (SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\LocalTesting;Initial Catalog=FrayData;Integrated Security=true;"))
            //{
            //    //AttachDbFilename=|DataDirectory|Database1.mdf;

            //    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.TOURNAMENT", sqlConnection);
            //    sqlConnection.Open();

            //    using (SqlDataReader reader = cmd.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            string sClientCode = reader["TOURNAMENT_ID"].ToString();
            //            string name = reader["TOURNAMENT_NAME"].ToString();
            //        }
            //    }
            //}

            //Data Source=.\SQLExpress;User Instance=true;User Id=UserName;Password=Secret;AttachDbFilename=|DataDirectory|Database1.mdf;
            //Data Source=.\SQLExpress;User Instance=true;Integrated Security=true;AttachDbFilename=|DataDirectory|Database1.mdf;
            //Data Source=tcp:qbstest.database.windows.net;Initial Catalog=QA_QPA_QFC;User ID=shawncutter;Password=fr057bit3!!

            //List<TournamentRetrieval> tournamentList = ChallongeDataHelper.GetAllTournaments();

            //Dictionary<int, List<MatchRetrieval>> tournyIdMatchDict = new Dictionary<int, List<MatchRetrieval>>();
            //Dictionary<int, List<ParticipantRetrieval>> tournyIdParticipantDict = new Dictionary<int, List<ParticipantRetrieval>>();

            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            //foreach(TournamentRetrieval tournament in tournamentList.Take(3))
            //{
            //    tournyIdMatchDict.Add(tournament.id, ChallongeDataHelper.GetTournamentMatches(tournament.id));
            //    tournyIdParticipantDict.Add(tournament.id, ChallongeDataHelper.GetTournamentParticipants(tournament.id));
            //}

            //sw.Stop();
        }
    }
}
