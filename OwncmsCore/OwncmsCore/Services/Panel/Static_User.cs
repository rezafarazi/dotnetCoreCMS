using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Static_User
{
    public class Static_User
    {

        public static users_tbl GetLogin(string username, string password)
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from users_tbl in DB.users_tbl where users_tbl.username.Equals(username) && users_tbl.password.Equals(password) && (users_tbl.user_type.Equals("ADMIN") || users_tbl.user_type.Equals("ADMIN_USER")) select users_tbl).FirstOrDefault();
            }
        }


        public static users_tbl GetLogin_With_Token(string token)
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from users_tbl in DB.users_tbl where users_tbl.login_token.Equals(token) && (users_tbl.user_type.Equals("ADMIN") || users_tbl.user_type.Equals("ADMIN_USER")) select users_tbl).FirstOrDefault();
            }
        }


        public static string GetProfileImageHash(long user_id)
        {
            using(Database_DB DB=new Database_DB())
            {
                var user = (from users_tbl in DB.users_tbl
                           join file_tbl in DB.file_tbl 
                           on users_tbl.profile_image_file_id.id equals file_tbl.id 
                           select new { user = users_tbl , profileimage = file_tbl }).First();

                if (user.profileimage.id == 0 || user.profileimage.id == 1)
                {
                    return "";
                }
                else
                {
                    return user.profileimage.file_hash;
                }
            }
        }


    }
}