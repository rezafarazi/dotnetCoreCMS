using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class settings_tbl
    {

        [Key]
        public long id { get; set; }
        public string parametrs { get; set; }
        public string discountall { get; set; }

    }
}
