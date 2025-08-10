using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using OwncmsCore.Middleware.AdminAuth;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Models.Data_Models.Update_Profile_Model;
using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Services.Global_Functions.Encript;
using OwncmsCore.Services.Panel.Notifications;
using OwncmsCore.Services.Panel.Static_User;
using OwncmsCore.Services.Panel.UserManager;
using OwncmsCore.Services.UserProfileImage;

namespace OwncmsCore.Controllers.Admin
{

    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminUserManagerController : Controller
    {


        //UserManager Start
        [Route("UserManager")]
        public async Task<ActionResult> User_Manager()
        {

            ViewBag.AdminUsers = UserManager.GET_ADMIN_USERS_DATA();
            ViewBag.Users = UserManager.GET_FILL_NEW_USERS();
            return View("../Admin/Dashboard/usermanager");
        }

        [HttpPost]
        [Route("UserManager")]
        public async Task<ActionResult> User_Manager(string User_Search)
        {

            ViewBag.AdminUsers = UserManager.GET_ADMIN_USERS_DATA();
            ViewBag.Users = UserManager.GET_USER_SEARCH(User_Search);
            return View("../Admin/Dashboard/usermanager");
        }


        [HttpGet]
        [Route("UserManager/Notifications/{User_Id}")]
        public async Task<ActionResult> Notificationss(string User_Id)
        {

            ViewBag.Notifications = Notifications.Get_All_Old_Notifications(long.Parse(User_Id));
            return View("../Admin/Dashboard/notifications");
        }

        [HttpGet]
        [Route("UserManager/SendNotification")]
        public async Task<ActionResult> SendNotification(string User_Id)
        {

            ViewBag.User_Id = User_Id;
            return View("../Admin/Dashboard/sendnotification");
        }


        [HttpPost]
        [Route("UserManager/SendNotification/Done")]
        public async Task<ActionResult> SendNotification(string User_Id, string title, string content_text, string link)
        {

            Notifications.Send_Notification(AdminAuth.AdminUser.ToString(), User_Id, title, content_text, link);
            return View("../Admin/Dashboard/done");
        }


        [HttpGet]
        [Route("UserManager/Edit_User/{User_Id}")]
        public async Task<ActionResult> Edit_User(string User_Id)
        {

            ViewBag.Edit_User = UserManager.GET_USER(int.Parse(User_Id), AdminAuth.AdminUser.id);
            return View("../Admin/Dashboard/user_profile_view");
        }

        [HttpPost]
        [Route("UserManager/Edit_User/User_Update")]
        public async Task<ActionResult> Update_User(Update_Profile_Model user)
        {

            string upload_path = await UserProfileImage.SaveUserProfileImage(user.profile_image);
            UserManager.UPDATE_USER_DATA(AdminAuth.AdminUser.id, user, upload_path);
            return View("../Admin/Dashboard/done");
        }
        //UserManager End


        //Profile Start
        [Route("Profile")]
        public async Task<ActionResult> Profile()
        {

            Refresh_User_Data();
            return View("../Admin/Dashboard/profile");
        }

        [HttpPost]
        [Route("Profile")]
        public async Task<ActionResult> Profile(Update_Profile_Model user_model)
        {


            string upload_path = await UserProfileImage.SaveUserProfileImage(user_model.profile_image);
            UserManager.Get_Update_Profile(user_model, upload_path);

            Refresh_User_Data();
            return Redirect(Request.Headers.Referer.ToString());
        }

        [Route("Profile/Active2F")]
        public async Task<ActionResult> Active2F(Update_Profile_Model user_model)
        {

            using (Database_DB DB = new Database_DB())
            {
                Users_Tbl user = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.login_token.Equals(AdminAuth.AdminUser.login_token) select Users_Tbl).First();

                string secure = generateSecret();

                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                byte[] secure_byte = System.Text.Encoding.ASCII.GetBytes(secure);

                user.google2f = secure;

                var qr_code = tfa.GenerateSetupCode("Sorapp", "Sorapp", secure_byte, 300, true);
                ViewBag.GoogleAuth2F = qr_code.QrCodeSetupImageUrl;

                DB.SaveChanges();

            }
            Refresh_User_Data();
            return View("../Admin/Dashboard/profile2F");
        }


        [Route("Profile/Passtive2F")]
        public async Task<ActionResult> Passtive2FPassword()
        {

            using (Database_DB DB = new Database_DB())
            {
                Users_Tbl user = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.login_token.Equals(AdminAuth.AdminUser.login_token) select Users_Tbl).First();
                user.google2f = "";
                DB.SaveChanges();
            }
            Refresh_User_Data();
            return Redirect("~/Admin/Panel/Profile");
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


        public void Refresh_User_Data()
        {
            Users_Tbl user = Static_User.GetLogin_With_Token(AdminAuth.AdminUser.login_token);
            string user_data = new JavaScriptSerializer().Serialize(user);
            string encript_user_data = Encript.Encrypt(user_data);
            Response.Cookies.Append("user_auth", encript_user_data);
        }
    }
}
