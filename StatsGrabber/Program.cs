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

            List<TournamentRetrieval> tournamentList = ChallongeHttpHelper.GetAllTournaments();

            int tournamentId = tournamentList.Last().id;

            List<MatchRetrieval> matchList = ChallongeHttpHelper.GetTournamentMatches(tournamentId);

            List<ParticipantRetrieval> participantList = ChallongeHttpHelper.GetTournamentParticipants(tournamentId);

            MatchRetrieval finalsMatch = matchList.OrderByDescending(x => x.round).First();
            ParticipantRetrieval winner = participantList.First(x => x.id.Equals(finalsMatch.winner_id));
        }
    }
}
