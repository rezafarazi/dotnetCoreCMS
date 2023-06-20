using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class discount_code_tbl
    {

        [Key]
        public long id { get; set; }
        public string code { get; set; }
        public int count { get; set; }
        public string delete_flag { get; set; }
        public string date { get; set; }

    }
}
