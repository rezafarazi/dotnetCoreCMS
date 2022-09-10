using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Static_User
{
    public class Static_User
    {

        public static Users_Tbl GetLogin(string username, string password)
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from Users_Tbl in DB.Users_Tbl where Users_Tbl.username.Equals(username) && Users_Tbl.password.Equals(password) && (Users_Tbl.user_type.Equals("ADMIN") || Users_Tbl.user_type.Equals("ADMIN_USER")) select Users_Tbl).FirstOrDefault();
            }
        }


        public static Users_Tbl GetLogin_With_Token(string token)
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from Users_Tbl in DB.Users_Tbl where Users_Tbl.login_token.Equals(token) && (Users_Tbl.user_type.Equals("ADMIN") || Users_Tbl.user_type.Equals("ADMIN_USER")) select Users_Tbl).FirstOrDefault();
            }
        }


    }
}