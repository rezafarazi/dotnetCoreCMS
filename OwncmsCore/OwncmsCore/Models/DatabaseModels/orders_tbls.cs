using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class orders_tbls
    {

        [Key]
        public long id { get; set; }
        public string type { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string order_status { get; set; }
        public string price { get; set; }
        public store_tbl store_id { get; set; }
        public users_tbl user_id { get; set; }

    }
}
