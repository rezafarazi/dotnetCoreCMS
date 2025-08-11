using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OwncmsCore.Models.DatabaseModels;
using Nancy.Json;
using OwncmsCore.Models.Data_Models.Auth_Model;

namespace OwncmsCore.Services.Global_Functions.User_JWT
{
    public class User_JWT
    {

        public static Auth_Model Get_User_From_JWT(string Encripted_JWT)
        {
            string json_user_data = OwncmsCore.Services.Global_Functions.Encript.Encript.Decrypt(Encripted_JWT);
            JObject user_data = JObject.Parse(json_user_data);
            Auth_Model user = new Auth_Model()
            {
                id = int.Parse(user_data["id"].ToString()),
                name = user_data["name"].ToString(),
                family = user_data["family"].ToString(),
                username = user_data["username"].ToString(),
                login_token = user_data["login_token"].ToString(),
                profile_image = user_data["profile_image"].ToString(),
            };

            return user;
        }



        public static string Get_User_To_Encripted_Jwt(Auth_Model user)
        {
            string user_data = new JavaScriptSerializer().Serialize(user);
            string encript_user_data = OwncmsCore.Services.Global_Functions.Encript.Encript.Encrypt(user_data);
            return encript_user_data;
        }


    }
}