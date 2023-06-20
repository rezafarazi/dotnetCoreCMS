using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class file_tbl
    {

        [Key]
        public long id { get; set; }
        public string file_name { get; set; }
        public string file_address { get; set; }
        public string file_address_address { get; set; }
        public string file_type { get; set; }
        public string file_extention { get; set; }
        public string file_size { get; set; }
        public string file_hash { get; set; }
        public string file_date_time { get; set; }

    }
}
