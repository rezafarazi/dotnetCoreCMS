using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class user_verify_tbl
    {

        [Key]
        public long id { get; set; }
        public string hash_code { get; set; }
        public users_tbl user_id { get; set; }

    }
}
