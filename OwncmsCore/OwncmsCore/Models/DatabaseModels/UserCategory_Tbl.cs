using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class UserCategory_Tbl
    {

        [Key]
        public long id { get; set; }
        public Users_Tbl user { get; set; }
        public Categories_Tbl category { get; set; }

    }
}