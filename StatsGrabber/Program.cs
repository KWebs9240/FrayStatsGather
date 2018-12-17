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
            var getResponse = ChallongeHttpHelper.BasicGet("tournaments.json");

            List<TournamentHolderGarbage> tournamentList = JsonConvert.DeserializeObject<List<TournamentHolderGarbage>>(getResponse);

            int tournamentId = tournamentList.First().tournament.id;
            int tournId2 = tournamentList[1].tournament.id;

            var matchGetResponse = ChallongeHttpHelper.BasicGet($"tournaments/{tournamentId}/matches.json");
            List<MatchHolderGarbage> matchList = JsonConvert.DeserializeObject<List<MatchHolderGarbage>>(matchGetResponse);

            var participantResponse = ChallongeHttpHelper.BasicGet($"tournaments/{tournamentId}/participants.json");
            List<ParticipantHolderGarbage> participantList = JsonConvert.DeserializeObject<List<ParticipantHolderGarbage>>(participantResponse);
        }
    }
}
