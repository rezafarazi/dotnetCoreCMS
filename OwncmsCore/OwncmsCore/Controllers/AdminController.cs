using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Models.DatabaseModels;
using Nancy.Json;
using OwncmsCore.Services.Panel;
using OwncmsCore.Services.Panel.Static_User;
using OwncmsCore.Services.Global_Functions.Encript;

namespace OwncmsCore.Controllers
{
    [Route("Admin")]
    [LanguageMiddle]
    public class AdminController : Controller
    {


        [Route("Login")]
        public IActionResult Login()
        {
            
            return View("Dashboard/login");
        }


        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(string username, string password)
        {
            using (Database_DB DB = new Database_DB())
            {
                string pass = Hasher_Lib.Hash.SHA_256(password);
                Users_Tbl user = Static_User.GetLogin(username, pass);
                if (user != null)
                {
                    if (user.google2f == null || user.google2f == "")
                    {
                        //Add Auth Session Start
                        string user_data = new JavaScriptSerializer().Serialize(user);
                        string encript_user_data = Encript.Encrypt(user_data);
                        Response.Cookies.Append("user_auth", encript_user_data);
                        //Add Auth Session End
                        return Redirect("~/Admin/Dashboard");
                    }
                    else
                    {
                        HttpContext.Session.SetString("user_token", user.login_token);
                        return Redirect("~/Admin/Login2F");
                    }
                }
                else
                {
                    ViewBag.Message_Alert = OwncmsCore.Language.all_strings.your_access_is_denid;
                }

                return Redirect("~/Admin/Login");
            }

        }



    }
}
