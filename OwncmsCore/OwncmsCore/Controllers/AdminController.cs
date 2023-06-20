using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Models.DatabaseModels;
using Nancy.Json;
using OwncmsCore.Services.Panel;
using OwncmsCore.Services.Panel.Static_User;
using OwncmsCore.Services.Global_Functions.Encript;
using Google.Authenticator;
using Newtonsoft.Json.Linq;
using MimeKit;
using System.Net.Mail;
using OwncmsCore.Models.Data_Models.Update_Profile_Model;

namespace OwncmsCore.Controllers
{

    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminController : Controller
    {

        //Global Variables Start
        public users_tbl User_Data;
        Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        //Global Variables End






        //Get Dashboard Start
        [Route("Dashboard")]
        public async Task<ActionResult> Dashboard()
        {
            GET_DATA();
            return View("Dashboard/index");
        }
        //Get Dashboard End




        //Logout Start
        [Route("Logout")]
        public async Task<ActionResult> Logout()
        {
            Response.Cookies.Delete("user_auth");
            Response.Redirect("~/Admin/Login");
            return Redirect("~/");
        }
        //Logout End




        //Get Data
        public void GET_DATA()
        {
            string user_auth = Request.Cookies["user_auth"].ToString();
            string json_user_data = Encript.Decrypt(user_auth);
            JObject user_data = JObject.Parse(json_user_data);
            users_tbl user = new users_tbl()
            {
                id = int.Parse(user_data["id"].ToString()),
                name = user_data["name"].ToString(),
                family = user_data["family"].ToString(),
                username = user_data["username"].ToString(),
                password = user_data["password"].ToString(),
                login_token = user_data["login_token"].ToString(),
                date_time = user_data["create_datetime"].ToString(),
                user_type = user_data["user_type"].ToString(),
                status = user_data["active_user_flag"].ToString(),
                email = user_data["email"].ToString(),
                address = user_data["address"].ToString(),
                phone = user_data["phone"].ToString()
            };

            User_Data = user;
            Check_Role(user, Request.Path.ToString());
            ViewBag.User_Data = User_Data;
        }

        public void Check_Role(users_tbl User, string Route)
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

            Logout();
            Redirect("~/Admin/Panel/Dashboard");

        }

        public void Refresh_User_Data()
        {
            users_tbl user = Static_User.GetLogin_With_Token(User_Data.login_token);
            string user_data = new JavaScriptSerializer().Serialize(user);
            string encript_user_data = Encript.Encrypt(user_data);
            Response.Cookies.Append("user_auth", encript_user_data);
        }

        public async Task<string> SaveUserProfileImage(IFormFile profileimage)
        {
            try
            {
                string path = "";
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Profiles"));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fileStream = new FileStream(Path.Combine(path, profileimage.FileName), FileMode.Create))
                {
                    await profileimage.CopyToAsync(fileStream);
                    return path + "\\" + profileimage.FileName;
                }
            }
            catch
            {

            }
            return "";
        }
        //My Functionss End


    }
}
