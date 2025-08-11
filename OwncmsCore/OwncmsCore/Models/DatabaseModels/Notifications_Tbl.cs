using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class notifications_tbl
    {

        [Key]
        public long id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string link { get; set; }
        public string from_user { get; set; }
        public bool seen { get; set; }
        public users_tbl user_id { get; set; }

    }
}
