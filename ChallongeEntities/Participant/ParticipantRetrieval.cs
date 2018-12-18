using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallongeEntities
{
    public class ParticipantRetrieval
    {
        public bool active { get; set; }
        public DateTime? checked_in_at { get; set; }
        public DateTime? created_at { get; set; }
        public int? final_rank { get; set; }
        public int? group_id { get; set; }
        public string icon { get; set; }
        public int id { get; set; }
        public int? invitation_id { get; set; }
        public string invite_email { get; set; }
        public string misc { get; set; }
        public string name { get; set; }
        public bool on_waiting_list { get; set; }
        public int seed { get; set; }
        public int tournament_id { get; set; }
        public DateTime? updated_at { get; set; }
        public string challonge_username { get; set; }
        public bool? challonge_email_address_verified { get; set; }
        public bool removable { get; set; }
        public bool partivipatable_or_invitation_attached { get; set; }
        public bool confirm_remove { get; set; }
        public bool invitation_pending { get; set; }
        public string display_name_with_invitation_email_address { get; set; }
        public string email_hash { get; set; }
        public string username { get; set; }
        public string attached_partivipatable_portrait_url { get; set; }
        public bool can_check_in { get; set; }
        public bool checked_in { get; set; }
        public bool reactivatable { get; set; }
    }
}
