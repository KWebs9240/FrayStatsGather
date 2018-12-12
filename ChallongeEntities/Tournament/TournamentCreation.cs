using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallongeEntities.Tournament
{
    public class TournamentCreation
    {
        public string name { get; set; }
        public string tournament_type { get;  set; }
        public string url { get; set; }
        public string subdomain { get; set; }
        public string description { get; set; }
        public bool open_signup { get; set; }
        public bool hold_third_place_match { get; set; }
        public decimal pts_for_match_win { get; set; }
        public decimal pts_for_match_tie { get; set; }
        public decimal pts_for_game_win { get; set; }
        public decimal pts_for_game_tie { get; set; }
        public decimal pts_for_bye { get; set; }
        public int swiss_rounds { get; set; }
        public string ranked_by { get; set; }
        public decimal rr_pts_for_match_win { get; set; }
        public decimal rr_pts_for_match_tie { get; set; }
        public decimal rr_pts_for_game_win { get; set; }
        public decimal rr_pts_for_game_tie { get; set; }
        public bool accept_attachments { get; set; }
        public bool hide_forum { get; set; }
        public bool show_rounds { get; set; }
        public bool @private { get; set; }
        public bool notify_users_when_matches_open { get; set; }
        public bool notify_users_when_the_tournament_ends { get; set; }
        public bool sequential_pairings { get; set; }
        public int? signup_cap { get; set; }
        public DateTime? start_at { get; set; }
        public int? check_in_duration { get; set; }
        public string grand_finals_modifier { get; set; }
    }
}
