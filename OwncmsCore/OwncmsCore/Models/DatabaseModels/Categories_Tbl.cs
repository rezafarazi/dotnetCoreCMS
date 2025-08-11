using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class categories_tbl
    {

        [Key]
        public long id { get; set; }
        public string title { get; set; }
        public int root_cat_id { get; set; }
        public string icon_address { get; set; }
        public string datetime { get; set; }
        public string castom_link { get; set; }
        public string visible { get; set; }
        public string delete_flag { get; set; }

    }
}
