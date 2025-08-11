using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Models.Data_Models.Auth_Model;
using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Services.Global_Functions.Encript;
using OwncmsCore.Services.Panel.Static_User;

namespace OwncmsCore.Controllers.Admin
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
                users_tbl user = Static_User.GetLogin(username, pass);
                if (user != null)
                {
                    string user_profile_image = Static_User.GetProfileImageHash(user.id);
                    if (user.google_auth == null || user.google_auth == "")
                    {
                        //Add Auth Session Start
                        Auth_Model user_auth = new Auth_Model()
                        {
                            id = user.id,
                            name = user.name,
                            family = user.family,
                            username = user.username,
                            login_token = user.login_token,
                            user_type = user.user_type,
                            profile_image = user_profile_image,
                        };
                        string user_data = new JavaScriptSerializer().Serialize(user_auth);
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

                users_tbl user = Static_User.GetLogin_With_Token(token);
                string user_profile_image = Static_User.GetProfileImageHash(user.id);

                byte[] secure_byte = System.Text.ASCIIEncoding.ASCII.GetBytes(user.google_auth);

                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();

                if (tfa.ValidateTwoFactorPIN(secure_byte, auth_2f_code))
                {
                    //Add Auth Session Start
                    Auth_Model user_auth = new Auth_Model()
                    {
                        id = user.id,
                        name = user.name,
                        family = user.family,
                        username = user.username,
                        login_token = user.login_token,
                        user_type = user.user_type,
                        profile_image = user_profile_image,
                    };
                    string user_data = new JavaScriptSerializer().Serialize(user_auth);
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
