using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OwncmsCore.Models.DatabaseModels;
using Nancy.Json;

namespace OwncmsCore.Services.Global_Functions.User_JWT
{
    public class User_JWT
    {

        public static Users_Tbl Get_User_From_JWT(string Encripted_JWT)
        {
            string json_user_data = OwncmsCore.Services.Global_Functions.Encript.Encript.Decrypt(Encripted_JWT);
            JObject user_data = JObject.Parse(json_user_data);
            Users_Tbl user = new Users_Tbl()
            {
                id = int.Parse(user_data["id"].ToString()),
                name = user_data["name"].ToString(),
                family = user_data["family"].ToString(),
                username = user_data["username"].ToString(),
                password = user_data["password"].ToString(),
                login_token = user_data["login_token"].ToString(),
                create_datetime = user_data["create_datetime"].ToString(),
                user_type = user_data["user_type"].ToString(),
                active_user_flag = user_data["active_user_flag"].ToString(),
                email = user_data["email"].ToString(),
                address = user_data["address"].ToString(),
                city = user_data["city"].ToString(),
                phone = user_data["phone"].ToString()
            };

            return user;
        }



        public static string Get_User_To_Encripted_Jwt(Users_Tbl user)
        {
            string user_data = new JavaScriptSerializer().Serialize(user);
            string encript_user_data = OwncmsCore.Services.Global_Functions.Encript.Encript.Encrypt(user_data);
            return encript_user_data;
        }


    }
}