using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class Users_Tbl
    {
        [Key]
        public long id { get; set; }
        public string name { get; set; } = "";
        public string family { get; set; } = "";
        public string username { get; set; } = "";
        public string password { get; set; } = "";
        public string login_token { get; set; } = "";
        public string create_datetime { get; set; } = "";
        public string user_type{ get; set; } = "";
        public string active_user_flag { get; set; } = "";
        public string email{ get; set; } = "";
        public string address { get; set; } = "";
        public string city { get; set; } = "";
        public string phone { get; set; } = "";
        public string google2f { get; set; } = "";
        public string national_id { get; set; } = "";
        public string father_name { get; set; } = "";
        public string profile_image_address { get; set; } = "";
        public bool notification { get; set; } = false;
    }
}