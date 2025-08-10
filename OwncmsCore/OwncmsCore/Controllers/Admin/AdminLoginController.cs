using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Services.Global_Functions.Encript;
using OwncmsCore.Services.Panel.Static_User;

namespace OwncmsCore.Controllers
{

    [Route("Admin")]
    [LanguageMiddle]
    public class AdminLoginController : Controller
    {






        //Login Start
        [Route("Login")]
        public async Task<ActionResult> Login()
        {
            try
            {
                if (Request.Cookies["user_auth"].ToString() != String.Empty)
                {
                    Response.Redirect("/Admin/Panel/Dashboard");
                }
            }
            catch
            {

            }
            return View("../Admin/Dashboard/login");
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
                        return Redirect("~/Admin/Panel/Dashboard");
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

        [Route("Login2F")]
        public async Task<ActionResult> Admin_Login2F()
        {
            return View("../Admin/Dashboard/login2f");
        }

        [HttpPost]
        [Route("Login2F")]
        public async Task<ActionResult> Admin_Login2F_Verify(string auth_2f_code)
        {
            using (Database_DB DB = new Database_DB())
            {
                string token = HttpContext.Session.GetString("user_token").ToString();

                Users_Tbl user = Static_User.GetLogin_With_Token(token);

                byte[] secure_byte = System.Text.ASCIIEncoding.ASCII.GetBytes(user.google2f);

                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();

                if (tfa.ValidateTwoFactorPIN(secure_byte, auth_2f_code))
                {
                    //Add Auth Session Start
                    string user_data = new JavaScriptSerializer().Serialize(user);
                    string encript_user_data = Encript.Encrypt(user_data);
                    Response.Cookies.Append("user_auth", encript_user_data);
                    //Add Auth Session End
                    Response.Redirect("~/Admin/Panel/Dashboard");
                }
            }

            return Redirect("~/Admin/Panel/Dashboard");
        }
        //Login End




        //Logout Start
        [Route("Logout")]
        public async Task<ActionResult> Logout()
        {
            Response.Cookies.Delete("user_auth");
            Response.Redirect("~/Admin/Login");
            return Redirect("~/");
        }
        //Logout End


    }
}
