using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class store_tbl
    {

        [Key]
        public long id { get; set; }
        public String title { get; set; }
        public String content{get; set;}
        public String type{get; set;}
        public String date{get; set;}
        public String delete_flag{get; set;}
        public String visible{get; set;}
        public String json{get; set;}
        public String price{get; set;}
        public String discount{get; set;}
        public String adiscount{get; set;}
        public users_tbl user_id { get; set; }
        public store_categories_tbl category_id { get; set; }
        public file_tbl file_id { get; set; }

    }
}
