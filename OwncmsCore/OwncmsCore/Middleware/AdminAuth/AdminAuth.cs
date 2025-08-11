using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OwncmsCore.Models.Data_Models.Auth_Model;
using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Services.Global_Functions.Encript;

namespace OwncmsCore.Middleware.AdminAuth
{
    public class AdminAuth
    {

        //Get user data
        public static Auth_Model UserData;        



        //Get Data
        public static void GET_DATA(HttpContext request)
        {
            string user_auth = request.Request.Cookies["user_auth"].ToString();
            string json_user_data = Encript.Decrypt(user_auth);
            JObject user_data = JObject.Parse(json_user_data);
            Auth_Model user = new Auth_Model()
            {
                id = int.Parse(user_data["id"].ToString()),
                name = user_data["name"].ToString(),
                family = user_data["family"].ToString(),
                username = user_data["username"].ToString(),
                login_token = user_data["login_token"].ToString(),
                user_type = user_data["user_type"].ToString(),
                profile_image = user_data["profile_image"].ToString(),
            };

            UserData = user;

            //ViewBag.User_Data = User_Data;
            //ViewBag.Notification = OwncmsCore.Services.Panel.Notifications.Notifications.Get_All_Notifications(user);
        }


        public static void Check_Role(HttpContext request, users_tbl User, string Route)
        {
            if (User.user_type == "ADMIN")
            {
                return;
            }

            string[] route = (Route + "/").Split('/');
            string route_loc = route[2];

            string[] ADMIN_ROUTES = { "Dashboard", "UserManager", "Reports", "AllNotWorkServices", "AllCurrentWorkServices", "Profile", "Backup", "Logout" };

            if (User.user_type == "ADMIN_USER")
            {
                if (ADMIN_ROUTES.Contains(route_loc))
                {
                    System.Diagnostics.Debug.WriteLine("True");
                    return;
                }
            }

            request.Response.Redirect("~/Admin/Logout");

        }


    }
}
