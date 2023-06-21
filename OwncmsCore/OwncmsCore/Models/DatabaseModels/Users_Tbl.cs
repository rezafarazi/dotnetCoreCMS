using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OwncmsCore.Models.DatabaseModels
{
    public class users_tbl
    {
        [Key]
        public long id { get; set; }
        public String username { get; set; }
        public String password{get;set;}
        public String name{get;set;}
        public String family{get;set;}
        public String phone{get;set;}
        public String email{get;set;}
        public String bio{get;set;}
        public String login_token{get;set;}
        public String date_time{get;set;}
        public String user_type{get;set;}
        public String status{get;set;}
        public String website{get;set;}
        public String address{get;set;}
        public String google_auth { get; set; }
        public city_tbls city_id { get; set; }
        public file_tbl profile_image_file_id { get; set; }

    }
}