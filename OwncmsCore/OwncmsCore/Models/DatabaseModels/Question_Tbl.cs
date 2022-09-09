using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class Question_Tbl
    {
        [Key]
        public long Id { get; set; }

        public Categories_Tbl Cat_Id { get; set; }
        public string Json_Questions { get; set; }
    }
}