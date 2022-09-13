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
using OwncmsCore.Services.Panel.Notifications;
using OwncmsCore.Services.Panel.FileManager;
using OwncmsCore.Services.Panel.Posts;
using static OwncmsCore.Services.Panel.Posts.Posts;

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
            ViewBag.AllCategories = OwncmsCore.Services.Panel.Categories.Categories.Select();

            if (!String.IsNullOrEmpty(Cat_Id))
            {
                ViewBag.EditCategory = OwncmsCore.Services.Panel.Categories.Categories.Select(long.Parse(Cat_Id));
                return View("Dashboard/editcategoreis");
            }
            else
            {
                return View("Dashboard/categoreis");
            }
        }

        [HttpPost]
        [Route("Categoreis/New")]
        public void New_Category(string Category_Id, string Category_Name, string Icon_Address, string Root_Cat, string Visibleity, string link, string TextData)
        {

            GET_DATA();
            OwncmsCore.Services.Panel.Categories.Categories.Insert(Category_Name, TextData, link, Icon_Address, (Root_Cat == "0") ? "MAIN" : "SUB", Visibleity, Root_Cat, User_Data);
            Response.Redirect("/Admin/Categoreis");
        }

        [HttpPost]
        [Route("Categoreis/Update")]
        public void Update_Category(string Category_Id, string Category_Name, string Icon_Address, string Root_Cat, string Visibleity, string link, string TextData)
        {

            GET_DATA();
            OwncmsCore.Services.Panel.Categories.Categories.Update(long.Parse(Category_Id), Category_Name, TextData, link, Icon_Address, (Root_Cat == "0") ? "MAIN" : "SUB", Visibleity, Root_Cat, User_Data);
            Response.Redirect("/Admin/Categoreis");
        }

        [Route("Categoreis/Delete")]
        public void Delete_Category([FromQuery] string Delete)
        {
            GET_DATA();
            OwncmsCore.Services.Panel.Categories.Categories.Delete(long.Parse(Delete));
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




        //UserManager Start
        [Route("UserManager")]
        public async Task<ActionResult> User_Manager()
        {
            GET_DATA();
            ViewBag.AdminUsers = UserManager.GET_ADMIN_USERS_DATA();
            ViewBag.Users = UserManager.GET_FILL_NEW_USERS();
            return View("Dashboard/usermanager");
        }

        [HttpPost]
        [Route("UserManager")]
        public async Task<ActionResult> User_Manager(string User_Search)
        {
            GET_DATA();
            ViewBag.AdminUsers = UserManager.GET_ADMIN_USERS_DATA();
            ViewBag.Users = UserManager.GET_USER_SEARCH(User_Search);
            return View("Dashboard/usermanager");
        }


        [HttpGet]
        [Route("UserManager/Notifications/{User_Id}")]
        public async Task<ActionResult> Notificationss(string User_Id)
        {
            GET_DATA();
            ViewBag.Notifications = Notifications.Get_All_Old_Notifications(long.Parse(User_Id));
            return View("Dashboard/notifications");
        }

        [HttpGet]
        [Route("UserManager/SendNotification")]
        public async Task<ActionResult> SendNotification(string User_Id)
        {
            GET_DATA();
            ViewBag.User_Id = User_Id;
            return View("Dashboard/sendnotification");
        }


        [HttpPost]
        [Route("UserManager/SendNotification/Done")]
        public async Task<ActionResult> SendNotification(string User_Id, string title, string content_text, string link)
        {
            GET_DATA();
            Notifications.Send_Notification(User_Data.ToString(), User_Id, title, content_text, link);
            return View("Dashboard/done");
        }


        [HttpGet]
        [Route("UserManager/Edit_User/{User_Id}")]
        public async Task<ActionResult> Edit_User(string User_Id)
        {
            GET_DATA();
            ViewBag.Edit_User = UserManager.GET_USER(int.Parse(User_Id), User_Data.id);
            return View("Dashboard/user_profile_view");
        }

        [HttpPost]
        [Route("UserManager/Edit_User/User_Update")]
        public async Task<ActionResult> Update_User(Update_Profile_Model user)
        {
            GET_DATA();
            string upload_path = await SaveUserProfileImage(user.profile_image);
            UserManager.UPDATE_USER_DATA(User_Data.id, user, upload_path);
            return View("Dashboard/done");
        }
        //UserManager End




        //Chart Start
        [Route("Charts")]
        public async Task<ActionResult> Charts()
        {
            GET_DATA();
            return View("Dashboard/charts");
        }
        //Chart End





        //File Manager Start
        [Route("FileManager")]
        public async Task<ActionResult> FileManager([FromQuery] string Dir)
        {
            GET_DATA();

            ViewBag.Dir = Dir;

            //Get Read Directory Start
            {
                if (Dir != String.Empty)
                {
                    GET_FILE_LIST(Dir);
                }
                else
                {
                    GET_FILE_LIST("");
                }
            }
            //Get Read Directory End

            return View("Dashboard/filemanager");
        }

        [HttpPost]
        [Route("New_Folder")]
        public async Task<ActionResult> New_Folder(string Folder_Name, string Addrs)
        {
            GET_DATA();
            //New Folder Event Start
            {
                string Addr = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Uploads/" + Addrs + "/" + Folder_Name));
                if (!Directory.Exists(Addr))
                    Directory.CreateDirectory(Addr);
            }
            //New Folder Event End

            Response.Redirect(Request.Headers.Referer.ToString());
            return View("Dashboard/index");
        }

        [HttpPost]
        [Route("Upload_File")]
        public async Task<ActionResult> Upload_File(IFormFile Upload_File, string Address)
        {
            GET_DATA();
            //Upload File Start
            {
                try
                {
                    if (Upload_File != null)
                    {
                        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Uploads/" + Address));

                        using (var fileStream = new FileStream(Path.Combine(path, Upload_File.FileName), FileMode.Create))
                        {
                            await Upload_File.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception Error)
                {
                }
            }
            //Upload File End

            Response.Redirect(Request.Headers.Referer);
            return View("Dashboard/index");
        }

        [Route("Delete_File")]
        public void Delete_File([FromQuery] string Delete)
        {
            string File_Address = Delete;

            if (System.IO.File.Exists(File_Address))
            {
                System.IO.File.Delete(File_Address);
            }
            else if (System.IO.Directory.Exists(File_Address))
            {
                System.IO.Directory.Delete(File_Address, true);
            }

            Response.Redirect(Request.Headers.Referer);
        }

        public void GET_FILE_LIST(string Address)
        {
            File_Manager.Files.Clear();

            string[] L_F_Files = Directory.GetFiles(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Uploads\\" + Address)));
            string[] L_D_Files = Directory.GetDirectories(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Uploads\\" + Address)));

            for (int i = 0; i < L_D_Files.Length; i++)
            {
                File_Manager_Items Item = new File_Manager_Items();
                string[] Dir_Name = (L_D_Files[i] + "\\").Split('\\');
                Item.Name = Dir_Name[Dir_Name.Length - 2];
                Item.Type = "Directory";
                Item.Address = L_D_Files[i];
                File_Manager.Files.Add(Item);
            }

            for (int i = 0; i < L_F_Files.Length; i++)
            {
                File_Manager_Items Item = new File_Manager_Items();
                string[] File_Name = (L_F_Files[i] + "\\").Split('\\');
                Item.Name = File_Name[File_Name.Length - 2];
                Item.Type = "File";
                Item.Address = L_F_Files[i];
                File_Manager.Files.Add(Item);
            }
        }
        //File Manager End







        //Get 404 Start
        [HttpGet]
        [Route("")]
        public void Not_Found()
        {
            GET_DATA();
            Response.Redirect("/Admin/Login");
        }
        //Get 404 End






        //New Post Start

        [Route("NewPost")]
        public async Task<ActionResult> NewPost()
        {
            GET_DATA();
            ViewBag.AllCategories = OwncmsCore.Services.Panel.Categories.Categories.Select();
            return View("Dashboard/newpost");
        }

        [Route("NewPostDone")]
        public async Task<ActionResult> AddNewPost(string title,string category,string visible,string img_adrs,string cntent)
        {
            GET_DATA();
            Posts.Insert(title, cntent, img_adrs, (visible=="1")?true:false, User_Data.id, long.Parse(category));
            return Redirect(Request.Headers.Referer.ToString());
        }

        //New Post End






        //AllPosts Start

        [Route("AllPosts")]
        public async Task<ActionResult> AllPosts()
        {
            GET_DATA();
            ViewBag.AllPosts = Posts.Select();
            return View("Dashboard/allposts");
        }


        [Route("AllPosts/{id}")]
        public async Task<ActionResult> GetPost(string id)
        {
            GET_DATA();
            PostData_struc post = Posts.Select(long.Parse(id));
            ViewBag.AllCategories = OwncmsCore.Services.Panel.Categories.Categories.Select();
            ViewBag.Post = post.Post;
            ViewBag.Cat = post.Category;
            return View("Dashboard/editpost");
        }


        [Route("AllPosts/Update")]
        public async Task<ActionResult> PostUpdate(string id,string title, string category, string visible, string img_adrs, string cntent)
        {
            GET_DATA();
            Posts.Update(long.Parse(id), title, cntent, img_adrs, (visible == "1") ? true : false, User_Data.id, long.Parse(category));
            return View("Dashboard/done");
        }


        [Route("AllPosts/Remove/{id}")]
        public async Task<ActionResult> PostRemove(string id)
        {
            GET_DATA();
            Posts.Delete(long.Parse(id));
            return Redirect(Request.Headers.Referer.ToString());
        }

        //AllPosts End







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
