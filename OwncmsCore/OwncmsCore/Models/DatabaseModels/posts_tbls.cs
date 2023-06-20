using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class posts_tbls
    {

        [Key]
        public long id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string image { get; set; }
        public string date { get; set; }
        public string delete_flag { get; set; }
        public string visible { get; set; }
        public users_tbl user_id { get; set; }
        public categories_tbl category_id { get; set; }


    }
}
