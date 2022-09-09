using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class Cites_Tbl
    {
        [Key]
        public long id { get; set; }
        public string city_name { get; set; }
        public string area_id { get; set; } = "0";
        public bool remove { get; set; } = false;
    }
}