using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class store_images_tbls
    {

        [Key]
        public long id { get; set; }
        public string date { get; set; }
        public store_tbl store_id { get; set; }
        public file_tbl file_id { get; set; }

    }
}
