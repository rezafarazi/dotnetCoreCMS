using OwncmsCore.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.Data_Models.Update_Profile_Model
{
    public class Update_Profile_Model
    {
        
        public string user_id { get; set; }
        public string username { get; set; } = "";
        public string name { get; set; } = "";
        public string family { get; set; } = "";
        public string password { get; set; } = "";
        public string email { get; set; } = "";
        public string phone { get; set; } = "";
        public string status { get; set; } = "";
        public string national_id { get; set; } = "";
        public string father_name { get; set; } = "";
        public string user_type { get; set; } = "";
        public string address { get; set; } = "";
        public string city { get; set; } = "";
        //public HttpPostedFileBase profile_image { get; set; }

    }
}