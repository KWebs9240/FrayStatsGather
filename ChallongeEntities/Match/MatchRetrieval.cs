using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallongeEntities.Match
{
    public class MatchRetrieval
    {
        public int? attachment_count { get; set; }
        public DateTime? created_at { get; set; }
        public int? group_id { get; set; }
        public bool has_attachment { get; set; }
        public int id { get; set; }
        public string identifier { get; set; }
        public string location { get; set; }
        public string loser_id { get; set; }
        public int? player1_id { get; set; }
        public bool player1_is_prereq_match_loser { get; set; }
        public int? player1_prereq_match_id { get; set; }
        public int? player1_votes { get; set; }
        public int? player2_id { get; set; }
        public bool player2_is_prereq_match_loser { get; set; }
        public int? player2_prereq_match_id { get; set; }
        public int? player2_votes { get; set; }
        public int round { get; set; }
        public DateTime? scheduled_time { get; set; }
        public DateTime? started_at { get; set; }
        public string state { get; set; }
        public int tournament_id { get; set; }
        public DateTime? underway_at { get; set; }
        public DateTime? updated_at { get; set; }
        public int? winner_id { get; set; }
        public string prerequisite_match_ids_csv { get; set; }
        public string scores_csv { get; set; }
    }
}
