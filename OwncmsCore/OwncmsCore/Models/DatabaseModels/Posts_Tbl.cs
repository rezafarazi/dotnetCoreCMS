using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class Posts_Tbl
    {

        [Key]
        public long id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string image { get; set; }
        public string date { get; set; }
        public bool delete_flag { get; set; }
        public bool visible { get; set; }
        public Users_Tbl User_Id { get; set; }
        public Categories_Tbl category_id { get; set; }

    }
}
