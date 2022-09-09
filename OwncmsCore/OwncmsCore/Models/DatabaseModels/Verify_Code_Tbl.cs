using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class Verify_Code_Tbl
    {

        public long id { get; set; }
        public Users_Tbl User { get; set; }
        public string code_hash { get; set; }

    }
}