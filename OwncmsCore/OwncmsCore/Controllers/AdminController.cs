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
using OwncmsCore.Services.Panel.Categories;
using OwncmsCore.Services.Panel.Statics;
using OwncmsCore.Services.Panel.FileManager;

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
            if (HttpContext.Session.Get("user_auth") != null)
            {
                Response.Redirect("~/Admin/Dashboard");
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
                string token = HttpContext.Session.Get("user_token").ToString();
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








        //Logout Start
        [Route("Logout")]
        public async Task<ActionResult> Logout()
        {
            HttpContext.Session.Remove("un");
            HttpContext.Session.Remove("up");
            HttpContext.Session.Remove("LGK");
            HttpContext.Session.Remove("TK");
            HttpContext.Session.Remove("user_auth");
            HttpContext.Session.Remove("ASPXAUTH");
            Response.Cookies.Delete("un");
            Response.Cookies.Delete("up");
            Response.Cookies.Delete("LGK");
            Response.Cookies.Delete("TK");
            Response.Cookies.Delete("user_auth");
            Response.Cookies.Delete(".ASPXAUTH");
            Response.Redirect("~/Admin/Login");
            return Redirect("~/");
        }
        //Logout End








        //Get Backup Start
        [Route("Backup")]
        public async Task<ActionResult> Backup()
        {
            GET_DATA();
            return View("Dashboard/backup");
        }






        //Categories Start
        [Route("Categoreis")]
        public async Task<ActionResult> Categories()
        {
            GET_DATA();
            Category_Class.Get_Generate_Categorys_Name();
            Category_Class.Get_Generate_Categprys_Tables_Data();

            if (HttpContext.Request.Query["Cat_Id"] != null)
            {
                Category_Class.Fill_Feilds_Records(int.Parse(HttpContext.Request.Query["Cat_Id"]));
            }

            ViewBag.All_City_And_Area = OwncmsCore.Services.Panel.City.City.Get_All_City_And_Area();

            return View("Dashboard/categoreis");
        }

        [HttpPost]
        [Route("Categoreis/New")]
        public void New_Category(string Category_Id, string Category_Name, string Icon_Address, string Header_Address, string TextData, string Root_Cat, string Visibleity, string city, string two_way)
        {

            GET_DATA();
            long city_code = long.Parse(city);

            //Create New Category Start
            {
                if (Category_Id == null || Category_Id == string.Empty)
                {
                    using (Database_DB DB = new Database_DB())
                    {
                        Category_Class.INSERT_NEW_CATEGORY(User_Data.id, Visibleity, Category_Name, Icon_Address, Root_Cat, city_code, Header_Address, TextData, two_way);
                    }
                }
                else
                {
                    Category_Class.UPDATE_Category(int.Parse(Category_Id), Category_Name, Icon_Address, (Root_Cat.ToUpper() == "ROOT") ? "" : Root_Cat, Visibleity, city_code, Header_Address, TextData, two_way);
                }
            }
            //Create New Category End

            Response.Redirect("/Admin/Categoreis");
        }

        [Route("Categoreis/Delete")]
        public void Delete_Category()
        {
            GET_DATA();
            //Delete Start
            {
                if (Request.QueryString.Get("Delete") != null)
                {
                    string Delete = Request.QueryString.Get("Delete");
                    Category_Class.Delete_CATEGORY(Delete);
                }
            }
            //Delete End

            Response.Redirect(Request.Headers["Referer"].ToString());
        }
        //Categories End







        //Chart Start
        [Route("Charts")]
        public async Task<ActionResult> Charts()
        {
            GET_DATA();
            ViewBag.Services_Data = 0;
            ViewBag.WeekServices = 0;
            return View("Dashboard/charts");
        }
        //Chart End






        //Settings Start
        [Route("Settings")]
        public async Task<ActionResult> Settings()
        {
            GET_DATA();
            ViewBag.Settings = OwncmsCore.Services.Global_Functions.Settings.Settings.Configs;
            return View("Dashboard/settings");
        }

        [HttpPost]
        [Route("Settings")]
        public async Task<ActionResult> Settings(string Config)
        {
            GET_DATA();
            OwncmsCore.Services.Global_Functions.Settings.Settings.Configs = Config;
            OwncmsCore.Services.Global_Functions.Settings.Settings.WriteSettings(Config);
            return Redirect("~/Admin/Settings");
        }
        //Settings End










        //New Report Start
        [Route("NewReport")]
        public async Task<ActionResult> Reports()
        {
            GET_DATA();
            ViewBag.Reports = OwncmsCore.Services.Panel.Reports.Reports.All_Reports();
            return View("Dashboard/new_report");
        }

        [HttpPost]
        [Route("NewReport")]
        public async Task<ActionResult> Reports(string title)
        {
            GET_DATA();
            OwncmsCore.Services.Panel.Reports.Reports.Add_New_Report(title);
            return Redirect("~/Admin/NewReport");
        }

        [HttpGet]
        [Route("NewReport/Remove/{id}")]
        public async Task<ActionResult> Reports_Remove(string id)
        {
            GET_DATA();
            OwncmsCore.Services.Panel.Reports.Reports.Remove_Report(long.Parse(id));
            return Redirect("~/Admin/NewReport");
        }
        //New Report End







        //Reports Start

        [Route("Reports")]
        public async Task<ActionResult> All_Reports()
        {
            GET_DATA();
            ViewBag.AllReports = OwncmsCore.Services.Panel.UsersReports.UsersReports.GetAll_User_Reports();
            return View("Dashboard/reports");
        }

        [HttpGet]
        [Route("Reports/View/{id}")]
        public async Task<ActionResult> All_Reports(string id)
        {
            GET_DATA();
            OwncmsCore.Services.Panel.UsersReports.UsersReports.GetView_Report(long.Parse(id));
            return Redirect("~/Admin/Reports");
        }

        //Reports End










        //File Manager Start
        [Route("FileManager")]
        public async Task<ActionResult> FileManager()
        {
            GET_DATA();
            //Get Read Directory Start
            {
                if (Request.QueryString.Get("Dir") != null)
                {
                    GET_FILE_LIST(Request.QueryString.Get("Dir"));
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
                string Addr = Server.MapPath("~/Uploads/" + Addrs + "/" + Folder_Name);
                if (!System.IO.Directory.Exists(Addr))
                    System.IO.Directory.CreateDirectory(Addr);
            }
            //New Folder Event End

            Response.Redirect(Request.Headers["Referer"].ToString());
            return View("Dashboard/index");
        }

        [HttpPost]
        [Route("Upload_File")]
        public async Task<ActionResult> Upload_File(HttpPostedFileBase Upload_File, string Address)
        {
            GET_DATA();
            //Upload File Start
            {
                try
                {
                    if (Upload_File != null)
                    {
                        string Upload_Address = Path.Combine(Server.MapPath("~/Uploads/" + Address), Path.GetFileName(Upload_File.FileName));
                        Upload_File.SaveAs(Upload_Address);
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

            Response.Redirect(Request.Headers["Referer"].ToString());
            return View("Dashboard/index");
        }

        [Route("Delete_File")]
        public void Delete_File()
        {
            string File_Address = Request.QueryString.Get("Delete");

            if (System.IO.File.Exists(File_Address))
            {
                System.IO.File.Delete(File_Address);
            }
            else if (System.IO.Directory.Exists(File_Address))
            {
                System.IO.Directory.Delete(File_Address, true);
            }

            Response.Redirect(Request.Headers["Referer"].ToString());
        }

        public void GET_FILE_LIST(string Address)
        {
            File_Manager.Files.Clear();

            string contentRootPath = _hostingEnvironment.ContentRootPath;

            string[] L_F_Files = Directory.GetFiles(_hostingEnvironment.ContentRootPath+"~/Uploads/" + Address);
            string[] L_D_Files = Directory.GetDirectories(_hostingEnvironment.ContentRootPath + "~/Uploads/" + Address);

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
        public async Task<ActionResult> Profile(OwncmsCore.Models.Data_Models.Update_Profile_Model.Update_Profile_Model user_model)
        {
            GET_DATA();
            OwncmsCore.Services.Panel.UserManager.UserManager.Get_Update_Profile(user_model,"");
            Refresh_User_Data();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Route("Profile/Active2F")]
        public async Task<ActionResult> Active2F(OwncmsCore.Models.Data_Models.Update_Profile_Model.Update_Profile_Model user_model)
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
        //Profile End







        //UserManager Start
        [Route("UserManager")]
        public async Task<ActionResult> User_Manager()
        {
            GET_DATA();
            ViewBag.AdminUsers = OwncmsCore.Services.Panel.UserManager.UserManager.GET_ADMIN_USERS_DATA();
            ViewBag.Users = OwncmsCore.Services.Panel.UserManager.UserManager.GET_FILL_NEW_USERS();
            return View("Dashboard/usermanager");
        }

        [HttpPost]
        [Route("UserManager")]
        public async Task<ActionResult> User_Manager(string User_Search)
        {
            GET_DATA();
            ViewBag.AdminUsers = OwncmsCore.Services.Panel.UserManager.UserManager.GET_ADMIN_USERS_DATA();
            ViewBag.Users = OwncmsCore.Services.Panel.UserManager.UserManager.GET_USER_SEARCH(User_Search);
            return View("Dashboard/usermanager");
        }


        [HttpGet]
        [Route("UserManager/Notifications/{User_Id}")]
        public async Task<ActionResult> Notifications(string User_Id)
        {
            GET_DATA();
            ViewBag.Notifications = OwncmsCore.Services.Panel.Notifications.Notifications.Get_All_Old_Notifications(long.Parse(User_Id));
            return View("Dashboard/notifications");
        }


        [HttpGet]
        [Route("UserManager/Pay30/{User_Id}")]
        public async Task<ActionResult> AddPay30(string User_Id)
        {
            GET_DATA();
            string value = Newtonsoft.Json.Linq.JObject.Parse(OwncmsCore.Services.Global_Functions.Settings.Settings.Configs.ToString()).GetValue("pay_30_day_value").ToString();
            OwncmsCore.Services.Panel.UserManager.UserManager.GetAdd30DayPeyment(long.Parse(User_Id), value, User_Data.id, 30);
            return View("Dashboard/done");
        }


        [HttpGet]
        [Route("UserManager/Pay7/{User_Id}")]
        public async Task<ActionResult> AddPay7(string User_Id)
        {
            GET_DATA();
            string value = Newtonsoft.Json.Linq.JObject.Parse(OwncmsCore.Services.Global_Functions.Settings.Settings.Configs.ToString()).GetValue("pay_30_day_value").ToString();
            OwncmsCore.Services.Panel.UserManager.UserManager.GetAdd30DayPeyment(long.Parse(User_Id), value, User_Data.id, 7);
            return View("Dashboard/done");
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
            OwncmsCore.Services.Panel.Notifications.Notifications.Send_Notification(User_Data.ToString(), User_Id, title, content_text, link);
            return View("Dashboard/done");
        }


        [HttpGet]
        [Route("UserManager/Edit_User/{User_Id}")]
        public async Task<ActionResult> Edit_User(string User_Id)
        {
            GET_DATA();
            ViewBag.Edit_User = OwncmsCore.Services.Panel.UserManager.UserManager.GET_USER(int.Parse(User_Id), User_Data.id);
            return View("Dashboard/user_profile_view");
        }


        [HttpPost]
        [Route("UserManager/Edit_User/User_Update")]
        public async Task<ActionResult> Update_User(OwncmsCore.Models.Data_Models.Update_Profile_Model.Update_Profile_Model user)
        {
            GET_DATA();
            OwncmsCore.Services.Panel.UserManager.UserManager.UPDATE_USER_DATA(User_Data.id, user, "");
            return View("Dashboard/done");
        }
        //UserManager End












        //Get 404 Start
        [HttpGet]
        [Route("")]
        public void Not_Found()
        {
            GET_DATA();
            Response.Redirect("/Admin/Login");
        }
        //Get 404 End








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


        public string SaveUserProfileImage(IFormFile profileimage)
        {
            //if (profileimage != null)
            //{
            //    string name = (DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Ticks + DateTime.Now.Day).ToString();
            //    string Upload_Address = Path.Combine(Server.MapPath("~/Uploads/Profiles"), name + Path.GetExtension(profileimage.FileName));
            //    System.Diagnostics.Debug.WriteLine(Upload_Address);
            //    profileimage.SaveAs(Upload_Address);

            //    return OwncmsCore.Language.all_strings.website + "/Uploads/Profiles/" + name + Path.GetExtension(profileimage.FileName);
            //}
            //else
            //{
                return "";
            //}
        }


        //My Functionss End




    }
}
