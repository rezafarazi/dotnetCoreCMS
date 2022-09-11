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
using OwncmsCore.Services.Panel.UserManager;
using OwncmsCore.Services.Panel.Categories;

namespace OwncmsCore.Controllers
{
    [Route("Admin")]
    [LanguageMiddle]
    public class AdminController : Controller
    {

        //Global Variables Start
        Users_Tbl User_Data;
        Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        //Global Variables End






        //Login Start
        [Route("Login")]
        public async Task<ActionResult> Login()
        {
            try
            {
                if (Request.Cookies["user_auth"].ToString() != String.Empty)
                {
                    Response.Redirect("/Admin/Dashboard");
                }
            }
            catch
            {

            }
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

        [Route("Login2F")]
        public async Task<ActionResult> Admin_Login2F()
        {
            return View("Dashboard/login2f");
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
                    Response.Redirect("~/Admin/Dashboard");
                }
            }

            return Redirect("~/Admin/Dashboard");
        }
        //Login End











        //Get Dashboard Start
        [Route("Dashboard")]
        public async Task<ActionResult> Index()
        {
            GET_DATA();
            return View("Dashboard/index");
        }
        //Get Dashboard End








        //Categories Start
        [Route("Categoreis")]
        public async Task<ActionResult> Categories([FromQuery] string Cat_Id)
        {
            GET_DATA();
            ViewBag.AllCategories = OwncmsCore.Services.Panel.Categories.Categories.SelectAllCategories();
            return View("Dashboard/categoreis");
        }

        [HttpPost]
        [Route("Categoreis/New")]
        public void New_Category(string Category_Id, string Category_Name, string Icon_Address, string Root_Cat, string Visibleity, string link,string text)
        {

            GET_DATA();
            OwncmsCore.Services.Panel.Categories.Categories.Insert(Category_Name, text, link, Icon_Address, (Root_Cat=="0")?"MAIN":"SUB", Visibleity, Root_Cat, User_Data);
            Response.Redirect("/Admin/Categoreis");
        }

        [Route("Categoreis/Delete")]
        public void Delete_Category([FromQuery] string Delete)
        {
            GET_DATA();
            
            Response.Redirect("/Admin/Categoreis");
        }
        //Categories End









        //Logout Start
        [Route("Logout")]
        public async Task<ActionResult> Logout()
        {
            Response.Cookies.Delete("user_auth");
            Response.Redirect("~/Admin/Login");
            return Redirect("~/");
        }
        //Logout End












        //Get 404 Start
        [HttpGet]
        [Route("")]
        public void Not_Found()
        {
            GET_DATA();
            Response.Redirect("/Admin/Login");
        }
        //Get 404 End





        //Profile Start
        [Route("Profile")]
        public async Task<ActionResult> Profile()
        {
            GET_DATA();
            Refresh_User_Data();
            return View("Dashboard/profile");
        }

        [HttpPost]
        [Route("Profile")]
        public async Task<ActionResult> Profile(Update_Profile_Model user_model)
        {
            GET_DATA();

            string upload_path = await SaveUserProfileImage(user_model.profile_image);
            UserManager.Get_Update_Profile(user_model, upload_path);

            Refresh_User_Data();
            return Redirect(Request.Headers.Referer.ToString());
        }

        [Route("Profile/Active2F")]
        public async Task<ActionResult> Active2F(Update_Profile_Model user_model)
        {
            GET_DATA();
            using (Database_DB DB = new Database_DB())
            {
                Users_Tbl user = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.login_token.Equals(User_Data.login_token) select Users_Tbl).First();

                string secure = generateSecret();

                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                byte[] secure_byte = System.Text.Encoding.ASCII.GetBytes(secure);

                user.google2f = secure;

                var qr_code = tfa.GenerateSetupCode("Sorapp", "Sorapp", secure_byte, 300, true);
                ViewBag.GoogleAuth2F = qr_code.QrCodeSetupImageUrl;

                DB.SaveChanges();

            }
            Refresh_User_Data();
            return View("Dashboard/profile2F");
        }


        [Route("Profile/Passtive2F")]
        public async Task<ActionResult> Passtive2FPassword()
        {
            GET_DATA();
            using (Database_DB DB = new Database_DB())
            {
                Users_Tbl user = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.login_token.Equals(User_Data.login_token) select Users_Tbl).First();
                user.google2f = "";
                DB.SaveChanges();
            }
            Refresh_User_Data();
            return Redirect("~/Admin/Profile");
        }


        private string generateSecret()
        {
            string random_secure = "";
            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                random_secure += rnd.Next(65, 75);
            }

            return random_secure;
        }
        //Profile Start






        //Get Data
        public void GET_DATA()
        {
            string user_auth = Request.Cookies["user_auth"].ToString();
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

            User_Data = user;
            Check_Role(user, Request.Path.ToString());
            ViewBag.User_Data = User_Data;
            ViewBag.Notification = OwncmsCore.Services.Panel.Notifications.Notifications.Get_All_Notifications(user);
        }


        public void Check_Role(Users_Tbl User, string Route)
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
            Redirect("~/Admin/Dashboard");

        }


        public void Refresh_User_Data()
        {
            Users_Tbl user = Static_User.GetLogin_With_Token(User_Data.login_token);
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
