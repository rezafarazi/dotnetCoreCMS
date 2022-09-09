using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class Transations_Tbl
    {
        [Key]
        public long id { get; set; }
        public Users_Tbl User { get; set; }
        public string value { get; set; }
        public string date { get; set; }
        public long peyment_user_id { get; set; } = 0;
    }
}