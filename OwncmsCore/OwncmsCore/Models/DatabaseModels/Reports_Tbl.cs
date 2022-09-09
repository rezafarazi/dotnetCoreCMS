using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class Reports_Tbl
    {

        [Key]
        public long id { get; set; }
        public string title { get; set; }
        public bool delete_flag { get; set; }

    }
}