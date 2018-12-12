using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallongeEntities.Tournament
{
    public static class TournamentConstants
    {
        public static class TournamentType
        {
            public static string SingleElimination = "single elimination";
            public static string DoubleElimination = "double elimination";
            public static string RoundRobin = "round robin";
            public static string Swiss = "swiss";
        }

        public static class RankedBy
        {
            public static string MatchWins = "match wins";
            public static string GameWins = "game wins";
            public static string PointsScored = "points scored";
            public static string PointsDifference = "points difference";
            public static string Custom = "custom";
        }

        public class GrandFinalsModifier
        {
            public static string Default = string.Empty;
            public static string SingleMatch = "single match";
            public static string Skip = "skip";
        }
    }
}
