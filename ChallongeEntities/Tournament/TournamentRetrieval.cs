using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallongeEntities.Tournament
{
    public class TournamentRetrieval
    {
        public bool accept_attachements { get; set; }
        public bool allow_participant_match_reporting { get; set; }
        public bool anonymous_voting { get; set; }
        public string category { get; set; }
        public int? check_in_duration { get; set; }
        public DateTime? completed_at { get; set; }
        public bool created_by_api { get; set; }
        public bool credit_capped { get; set; }
        public string description { get; set; }
        public int? game_id { get; set; }
        public bool group_stages_enabled { get; set; }
        public bool hide_forum { get; set; }
        public bool hide_seeds { get; set; }
        public bool hold_third_place_match { get; set; }
        public int id { get; set; }
        public int? max_predictions_per_user { get; set; }
        public string name { get; set; }
        public bool notify_users_when_matches_open { get; set; }
        public bool notify_users_when_the_tournament_ends { get; set; }
        public bool open_signup { get; set; }
        public int participants_count { get; set; }
        public int predictions_method { get; set; }
        public DateTime? predictions_opened_at { get; set; }
        public bool @private { get; set; }
        public int progress_meter { get; set; }
        public decimal pts_for_bye { get; set; }
        public decimal pts_for_game_tie { get; set; }
        public decimal pts_for_game_win { get; set; }
        public decimal pts_for_match_tie { get; set; }
        public decimal pts_for_match_win { get; set; }
        public bool quick_advance { get; set; }
        public string ranked_by { get; set; }
        public bool require_score_agreement { get; set; }
        public decimal rr_pts_for_game_tie { get; set; }
        public decimal rr_pts_for_game_win { get; set; }
        public decimal rr_pts_for_match_tie { get; set; }
        public decimal rr_pts_for_match_win { get; set; }
        public bool sequential_pairings { get; set; }
        public bool show_rounds { get; set; }
        public int? signup_cap { get; set; }
        public DateTime? start_at { get; set; }
        public DateTime? started_at { get; set; }
        public DateTime? started_checking_in_at { get; set; }
        public string state { get; set; }
        public int swiss_rounds { get; set; }
        public bool? teams { get; set; }
        public List<string> tie_breaks { get; set; }
        public string tournament_type { get; set; }
        public DateTime updated_at { get; set; }
        public string url { get; set; }
        public string description_source { get; set; }
        public string subdomain { get; set; }
        public string full_challonge_url { get; set; }
        public string live_image_url { get; set; }
        public string sign_up_url { get; set; }
        public bool review_before_finalizing { get; set; }
        public bool accepting_predictions { get; set; }
        public bool participants_locked { get; set; }
        public string game_name { get; set; }
        public bool participants_swappable { get; set; }
        public bool team_convertable { get; set; }
        public bool group_stages_were_started { get; set; }
    }
}
