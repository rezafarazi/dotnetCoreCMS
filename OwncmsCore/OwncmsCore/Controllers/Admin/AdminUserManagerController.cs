using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using OwncmsCore.Middleware.AdminAuth;
using OwncmsCore.Middleware.Language;
<<<<<<< HEAD
using OwncmsCore.Models.Data_Models.Auth_Model;
using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Models.FormModels;
using OwncmsCore.Services.Global_Functions.Encript;
using OwncmsCore.Services.Panel.Groups;
using OwncmsCore.Services.Panel.Notifications;
using OwncmsCore.Services.Panel.Static_User;
using OwncmsCore.Services.Panel.User;
using OwncmsCore.Services.Panel.Usermanager;

namespace OwncmsCore.Controllers.Admin
{
=======
using OwncmsCore.Models.Data_Models.Update_Profile_Model;
using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Services.Global_Functions.Encript;
using OwncmsCore.Services.Panel.Notifications;
using OwncmsCore.Services.Panel.Static_User;
using OwncmsCore.Services.Panel.UserManager;
using OwncmsCore.Services.UserProfileImage;

namespace OwncmsCore.Controllers.Admin
{

>>>>>>> master
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminUserManagerController : Controller
    {


        //UserManager Start
        [Route("UserManager")]
        public async Task<ActionResult> User_Manager()
        {

<<<<<<< HEAD
            ViewBag.AdminUsers = Usermanager_Service.GET_ADMIN_USERS_DATA();
            ViewBag.Users = Usermanager_Service.GET_FILL_NEW_USERS();
=======
            ViewBag.AdminUsers = UserManager.GET_ADMIN_USERS_DATA();
            ViewBag.Users = UserManager.GET_FILL_NEW_USERS();
>>>>>>> master
            return View("../Admin/Dashboard/usermanager");
        }

        [HttpPost]
        [Route("UserManager")]
        public async Task<ActionResult> User_Manager(string User_Search)
        {

<<<<<<< HEAD
            ViewBag.AdminUsers = Usermanager_Service.GET_ADMIN_USERS_DATA();
            ViewBag.Users = Usermanager_Service.GET_USER_SEARCH(User_Search);
            return View("../Admin/Dashboard/usermanager");
        }

=======
            ViewBag.AdminUsers = UserManager.GET_ADMIN_USERS_DATA();
            ViewBag.Users = UserManager.GET_USER_SEARCH(User_Search);
            return View("../Admin/Dashboard/usermanager");
        }


>>>>>>> master
        [HttpGet]
        [Route("UserManager/Notifications/{User_Id}")]
        public async Task<ActionResult> Notificationss(string User_Id)
        {

<<<<<<< HEAD
            ViewBag.Notifications = Notification_Service.Get_All_Old_Notifications(long.Parse(User_Id));
=======
            ViewBag.Notifications = Notifications.Get_All_Old_Notifications(long.Parse(User_Id));
>>>>>>> master
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

<<<<<<< HEAD
            Notification_Service.Send_Notification(AdminAuth.UserData.ToString(), User_Id, title, content_text, link);
            return View("../Admin/Dashboard/done");
        }

=======
            Notifications.Send_Notification(AdminAuth.AdminUser.ToString(), User_Id, title, content_text, link);
            return View("../Admin/Dashboard/done");
        }


>>>>>>> master
        [HttpGet]
        [Route("UserManager/Edit_User/{User_Id}")]
        public async Task<ActionResult> Edit_User(string User_Id)
        {

<<<<<<< HEAD
            Static_User.UserCity user = Static_User.GetUserByID(long.Parse(User_Id));
            users_tbl usr = user.user;
            ViewBag.USER = usr;
            ViewBag.City = user.city;
=======
            ViewBag.Edit_User = UserManager.GET_USER(int.Parse(User_Id), AdminAuth.AdminUser.id);
>>>>>>> master
            return View("../Admin/Dashboard/user_profile_view");
        }

        [HttpPost]
<<<<<<< HEAD
        [Route("UserManager/UpdateDone")]
        public async Task<ActionResult> UpdateDone(Profile_Model profile, IFormFile profile_image)
        {

            if (profile_image != null)
            {
                profile.profile_image = profile_image;
            }


            profile.id = AdminAuth.UserData.id;
            users_tbl user = await Users_Service.UpdateUser(profile);

=======
        [Route("UserManager/Edit_User/User_Update")]
        public async Task<ActionResult> Update_User(Update_Profile_Model user)
        {

            string upload_path = await UserProfileImage.SaveUserProfileImage(user.profile_image);
            UserManager.UPDATE_USER_DATA(AdminAuth.AdminUser.id, user, upload_path);
>>>>>>> master
            return View("../Admin/Dashboard/done");
        }
        //UserManager End


<<<<<<< HEAD

        //Groups start
        [Route("Groups")]
        public async Task<ActionResult> Groups()
        {

            ViewBag.Groups = Groups_Service.GetAllAccessTypeUser();
            return View("../Admin/Dashboard/groups");
        }

        [HttpPost]
        [Route("Groups/New")]
        public async Task<ActionResult> NewGroups(string title, string en_title)
        {

            Groups_Service.GetAddNewGroup(title, en_title);
            return Redirect(Request.Headers.Referer);
        }

        [HttpGet]
        [Route("Groups/Delete/{type_id}")]
        public async Task<ActionResult> RemoveGroups(string type_id)
        {

            Groups_Service.GetRemoveAccessType(int.Parse(type_id));
            return Redirect(Request.Headers.Referer);
        }
        //Groups end



        //Profile start
=======
        //Profile Start
>>>>>>> master
        [Route("Profile")]
        public async Task<ActionResult> Profile()
        {

<<<<<<< HEAD
            Static_User.UserCity user = Static_User.GetUserByID(AdminAuth.UserData.id);
            users_tbl usr = user.user;
            ViewBag.USER = usr;
            ViewBag.City = user.city;
=======
            Refresh_User_Data();
>>>>>>> master
            return View("../Admin/Dashboard/profile");
        }

        [HttpPost]
<<<<<<< HEAD
        [Route("Profile/Update")]
        public async Task<ActionResult> ProfileDone(Profile_Model profile, IFormFile profile_image)
        {

            if (profile_image != null)
            {
                profile.profile_image = profile_image;
            }


            profile.id = AdminAuth.UserData.id;
            users_tbl user = await Users_Service.UpdateUser(profile);
            string user_profile_image = Static_User.GetProfileImageHash(user.id);

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

            return Redirect(Request.Headers.Referer);
        }

        [Route("Profile/Active2F")]
        public async Task<ActionResult> Active2F()
        {

            ViewBag.USER = Static_User.GetLogin_With_Token(AdminAuth.UserData.login_token);

            using (Database_DB DB = new Database_DB())
            {
                users_tbl user = (from Users_Tbl in DB.users_tbl where Users_Tbl.login_token.Equals(AdminAuth.UserData.login_token) select Users_Tbl).First();
=======
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
>>>>>>> master

                string secure = generateSecret();

                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                byte[] secure_byte = System.Text.Encoding.ASCII.GetBytes(secure);

<<<<<<< HEAD
                user.google_auth = secure;
=======
                user.google2f = secure;
>>>>>>> master

                var qr_code = tfa.GenerateSetupCode("Sorapp", "Sorapp", secure_byte, 300, true);
                ViewBag.GoogleAuth2F = qr_code.QrCodeSetupImageUrl;

                DB.SaveChanges();

            }
<<<<<<< HEAD

            return View("../Admin/Dashboard/profile2F");
        }

        [Route("Profile/Diactive2F")]
        public async Task<ActionResult> Diactive2F()
=======
            Refresh_User_Data();
            return View("../Admin/Dashboard/profile2F");
        }


        [Route("Profile/Passtive2F")]
        public async Task<ActionResult> Passtive2FPassword()
>>>>>>> master
        {

            using (Database_DB DB = new Database_DB())
            {
<<<<<<< HEAD
                users_tbl user = (from Users_Tbl in DB.users_tbl where Users_Tbl.login_token.Equals(AdminAuth.UserData.login_token) select Users_Tbl).First();
                user.google_auth = "";
                DB.SaveChanges();
            }

            return Redirect(Request.Headers.Referer.ToString());
        }
        //Profile end

=======
                Users_Tbl user = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.login_token.Equals(AdminAuth.AdminUser.login_token) select Users_Tbl).First();
                user.google2f = "";
                DB.SaveChanges();
            }
            Refresh_User_Data();
            return Redirect("~/Admin/Panel/Profile");
        }
>>>>>>> master


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
<<<<<<< HEAD
        //My Functionss End

=======
        //Profile Start


        public void Refresh_User_Data()
        {
            Users_Tbl user = Static_User.GetLogin_With_Token(AdminAuth.AdminUser.login_token);
            string user_data = new JavaScriptSerializer().Serialize(user);
            string encript_user_data = Encript.Encrypt(user_data);
            Response.Cookies.Append("user_auth", encript_user_data);
        }
>>>>>>> master
    }
}
