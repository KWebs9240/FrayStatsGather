using ChallongeApiHelper;
using ChallongeEntities.Match;
using ChallongeEntities.Participant;
using ChallongeEntities.Tournament;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            string thing = ConfigurationManager.AppSettings["ApiUsername"];
            ChallongeHttpHelper.setAuthorizationHeader(ConfigurationManager.AppSettings["ApiUsername"], ConfigurationManager.AppSettings["ApiPassword"]);

            List<TournamentHolderGarbage> tournamentList = ChallongeHttpHelper.GetAllTournaments();

            int tournamentId = tournamentList.First().tournament.id;
            int tournId2 = tournamentList[1].tournament.id;

            List<MatchHolderGarbage> matchList = ChallongeHttpHelper.GetTournamentMatches(tournamentId);

            List<ParticipantHolderGarbage> participantList = ChallongeHttpHelper.GetTournamentParticipants(tournamentId);
        }
    }
}
