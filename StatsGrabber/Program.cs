using ChallongeApiHelper;
using ChallongeApiHelper.DataHelper;
using ChallongeEntities;
using CoreStatsGather;
using FrayStatsDbEntities;
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
            //Looks like it might have been some good ol git configs

            //FrayDbTournament test = new FrayDbTournament()
            //{
            //    TournamentId = 1,
            //    TournamentName = "Test Save Code",
            //    TournamentDt = DateTime.UtcNow
            //};

            //ChallongeApiHelper.SQLHelper.ChallongeSQLHelper.SqlSaveTournament(test);

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

            List<TournamentRetrieval> tournamentList = ChallongeDataHelper.GetAllTournaments();

            TournamentRetrieval tournament = tournamentList.First();
            List<MatchRetrieval> tournamentMatches = ChallongeDataHelper.GetTournamentMatches(tournament.id);
            List<ParticipantRetrieval> tournamentParticipants = ChallongeDataHelper.GetTournamentParticipants(tournament.id);

            FrayDbTournament dbTournament = new FrayDbTournament()
            {
                TournamentName = tournament.name,
                TournamentId = tournament.id,
                TournamentDt = tournament.started_at.Value
            };


        }
    }
}
