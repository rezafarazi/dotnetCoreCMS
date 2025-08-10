using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Services.Global_Functions.Encript;

namespace OwncmsCore.Middleware.AdminAuth
{
    public class AdminAuth
    {

        //Get user data
        public static Users_Tbl AdminUser;
        public static OwncmsCore.Services.Panel.Notifications.Notifications.Notification_Out Notification;



        //Get Data
        public static void GET_DATA(HttpContext request)
        {
            string user_auth = request.Request.Cookies["user_auth"].ToString();
            string json_user_data = Encript.Decrypt(user_auth);
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
                phone = user_data["phone"].ToString(),
                google2f = user_data["google2f"].ToString(),
                national_id = user_data["national_id"].ToString(),
                father_name = user_data["father_name"].ToString(),
                profile_image_address = user_data["profile_image_address"].ToString()
            };

            AdminUser = user;
            Notification = OwncmsCore.Services.Panel.Notifications.Notifications.Get_All_Notifications(user);

            Check_Role(request,user, request.Request.Path.ToString());            

            //ViewBag.User_Data = User_Data;
            //ViewBag.Notification = OwncmsCore.Services.Panel.Notifications.Notifications.Get_All_Notifications(user);
        }


        public static void Check_Role(HttpContext request, Users_Tbl User, string Route)
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
