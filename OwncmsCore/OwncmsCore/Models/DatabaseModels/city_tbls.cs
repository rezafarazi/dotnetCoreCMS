using System.ComponentModel.DataAnnotations;

namespace OwncmsCore.Models.DatabaseModels
{
    public class city_tbls
    {
        [Key]
        public long id { get; set; }
        public string name { get; set; }
        public int area { get; set; }
        public string status { get; set; }
        public string datetime { get; set; }

    }
}
