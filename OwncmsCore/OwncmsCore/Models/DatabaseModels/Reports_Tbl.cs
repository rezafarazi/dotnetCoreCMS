using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class reports_tbl
    {

        [Key]
        public long id { get; set; }
        public string content { get; set; }
        public string date_time { get; set; }
        public bool condition { get; set; }
        public report_tbl report_id { get; set; }
        public users_tbl user_id { get; set; }
        public posts_tbls post_id { get; set; }

    }
}
