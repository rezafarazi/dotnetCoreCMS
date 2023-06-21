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
using OwncmsCore.Models.Data_Models.Auth_Model;

namespace OwncmsCore.Controllers
{

    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminController : Controller
    {

        //Global Variables Start
        public Auth_Model User_Data;
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

            User_Data = user;
            ViewBag.User_Data = User_Data;
        }
        //My Functionss End


    }
}
