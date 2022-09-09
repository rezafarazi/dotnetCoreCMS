using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.Data_Models.User_Update_Model
{
    public class User_Update_Model
    {
        public string User_Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } = "";
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Profile_Image { get; set; }
        public string User_Type { get; set; }
        public string User_Active { get; set; }
        public string father_name { get; set; }
        public string national_id { get; set; }
        public string city { get; set; }
        public string address { get; set; }
    }
}