using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class Services_Tbl
    {

        [Key]
        public long id { get; set; }
        public string data { get; set; } = "";
        public string ditales { get; set; } = "";
        public string lat { get; set; } = "";
        public string lng { get; set; } = "";
        public Users_Tbl req_user { get; set; }
        public long worker_user { get; set; } = 0;
        public string peyment_money { get; set; } = "";
        public string rate { get; set; } = "0";
        public string start_datetime { get; set; } = "";
        public string over_datetime { get; set; } = "";
        public string status { get; set; } = "";
        public Categories_Tbl category { get; set; }

    }
}