using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class transcation_tbl
    {

        [Key]
        public long id { get; set; }
        public string value { get; set; }
        public string date { get; set; }
        public users_tbl user_id { get; set; }
        public orders_tbls order_id { get; set; }

    }
}
