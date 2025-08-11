using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class report_tbl
    {

        [Key]
        public long id { get; set; }
        public string title { get; set; }
        public bool delete_flag { get; set; }

    }
}
