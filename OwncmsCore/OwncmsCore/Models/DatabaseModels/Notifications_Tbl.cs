using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class Notifications_Tbl
    {
        [Key]
        public long id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string link { get; set; }
        public string from { get; set; }
        public string seen { get; set; }
        public Users_Tbl user_id { get; set; }
    }
}