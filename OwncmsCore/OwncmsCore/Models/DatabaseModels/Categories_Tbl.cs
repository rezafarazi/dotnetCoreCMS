using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class Categories_Tbl
    {
        [Key]
        public long id { get; set; }
        public string title { get; set; }
        public string Icon_Address { get; set; }
        public string Type { get; set; }
        public string Create_Date { get; set; }
        public string Enabled_Flag { get; set; }
        public string Root_Id { get; set; }
        public Users_Tbl User_Id { get; set; }
        public string text { get; set; }
        public string link { get; set; }
    }
}