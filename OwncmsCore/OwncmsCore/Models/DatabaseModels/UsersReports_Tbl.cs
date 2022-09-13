using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class UsersReports_Tbl
    {

        [Key]
        public long id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string DateTime { get; set; }
        public bool Seen { get; set; }
        public Users_Tbl Reporter_User { get; set; }
        public Posts_Tbl report_post { get; set; }
        public Reports_Tbl Report_Type { get; set; }

    }
}