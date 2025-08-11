using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class userstype_tbl
    {

        [Key]
        public long id { get; set; }
        public string title { get; set; }
        public string en_title { get; set; }

    }
}
