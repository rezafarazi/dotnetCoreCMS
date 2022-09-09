using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class Logs_Tbl
    {
        [Key]
        public long id { get; set; }
        public Users_Tbl User_Id { get; set; }
        public string DateTime { get; set; }
    }
}