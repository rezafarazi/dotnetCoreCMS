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
using OwncmsCore.Services.Panel.Categories;
using OwncmsCore.Models.FormModels;
using OwncmsCore.Services.Panel.Cites;
using OwncmsCore.Services.Panel.User;
using OwncmsCore.Services.Panel.Settings;
using OwncmsCore.Services.Panel.FileManager;
using OwncmsCore.Services.Panel.Groups;
using OwncmsCore.Services.Panel.Report;
using OwncmsCore.Services.Panel.Post;
using static OwncmsCore.Services.Panel.Post.Post_Service;

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






        //Get Cateogries Start
        [Route("Categoreis")]
        public async Task<ActionResult> Categoreis()
        {
            GET_DATA();
            ViewBag.Categories = Categories_Service.GetAllCategories();
            return View("Dashboard/categories");
        }

        [HttpPost]
        [Route("Categoreis/New")]
        public async Task<ActionResult> NewCategory(Category_Model category)
        {
            Categories_Service.NewCategory(category);
            return Redirect("~/Admin/Panel/Categoreis");
        }

        [HttpPost]
        [Route("Categoreis/UpdateDone")]
        public async Task<ActionResult> UpdateCategoryDone(Category_Model category)
        {
            Categories_Service.UpdateCategory(category);
            return Redirect("~/Admin/Panel/Categoreis");
        }

        [HttpGet]
        [Route("Category/Remove/{category_id}")]
        public async Task<ActionResult> DeleteCategory(string category_id)
        {
            Categories_Service.DeleteCategory(long.Parse(category_id));
            return Redirect(Request.Headers.Referer.ToString());
        }

        [HttpGet]
        [Route("Category/Update/{category_id}")]
        public async Task<ActionResult> UpdateCategory(string category_id)
        {
            GET_DATA();
            ViewBag.Categories = Categories_Service.GetAllCategories();
            ViewBag.Category = Categories_Service.GetCategory(long.Parse(category_id));
            return View("Dashboard/categories");
        }
        //Get Categories End









        //New Post Start

        [Route("NewPost")]
        public async Task<ActionResult> NewPost()
        {
            GET_DATA();
            ViewBag.AllCategories = Categories_Service.GetAllCategories();
            return View("Dashboard/newpost");
        }

        [Route("NewPostDone")]
        public async Task<ActionResult> AddNewPost(string title, string category, string visible, string img_adrs, string cntent)
        {
            GET_DATA();
            Post_Service.Insert(title, cntent, img_adrs, (visible == "1") ? true : false, User_Data.id, long.Parse(category));
            return Redirect(Request.Headers.Referer.ToString());
        }

        //New Post End









        //AllPosts Start

        [Route("AllPosts")]
        public async Task<ActionResult> AllPosts()
        {
            GET_DATA();
            ViewBag.AllPosts = Post_Service.Select();
            return View("Dashboard/allposts");
        }


        [Route("AllPosts/{id}")]
        public async Task<ActionResult> GetPost(string id)
        {
            GET_DATA();
            PostData_struc post = Post_Service.Select(long.Parse(id));
            ViewBag.AllCategories = Categories_Service.GetAllCategories();
            ViewBag.Post = post.Post;
            ViewBag.Cat = post.Category;
            return View("Dashboard/editpost");
        }


        [Route("AllPosts/Update")]
        public async Task<ActionResult> PostUpdate(string id, string title, string category, string visible, string img_adrs, string cntent)
        {
            GET_DATA();
            Post_Service.Update(long.Parse(id), title, cntent, img_adrs, (visible == "1") ? true : false, User_Data.id, long.Parse(category));
            return View("Dashboard/done");
        }


        [Route("AllPosts/Remove/{id}")]
        public async Task<ActionResult> PostRemove(string id)
        {
            GET_DATA();
            Post_Service.Delete(long.Parse(id));
            return Redirect(Request.Headers.Referer.ToString());
        }

        //AllPosts End








        //Get Chart Start
        [Route("Charts")]
        public async Task<ActionResult> Chart()
        {
            GET_DATA();
            return View("Dashboard/chart");
        }
        //Get Chart End










        //Get Chart Start
        [Route("Cities")]
        public async Task<ActionResult> Cities()
        {
            GET_DATA();
            return View("Dashboard/cites");
        }

        [HttpGet]
        [Route("Cities/Edit/{city_id}")]
        public async Task<ActionResult> Cities(string city_id)
        {
            GET_DATA();
            ViewBag.City_Data = City_Service.GetCity(long.Parse(city_id));
            return View("Dashboard/cites");
        }

        [HttpPost]
        [Route("Cities/New")]
        public async Task<ActionResult> NewCity(string name,string area,string status)
        {
            City_Service.AddNewCityOrArea(name, int.Parse(area), status);
            return Redirect("/Admin/Panel/Cities");
        }

        [HttpPost]
        [Route("Cities/Update")]
        public async Task<ActionResult> UpdateCity(string id,string name, string area, string status)
        {
            City_Service.UpdateCityOrArea(long.Parse(id),name, int.Parse(area), status);
            return Redirect("/Admin/Panel/Cities");
        }
        //Get Chart End












        //Reports Start

        [Route("Reports")]
        public async Task<ActionResult> All_Reports()
        {
            GET_DATA();
            ViewBag.AllReports = Report_Service.GetAll_User_Reports();
            return View("Dashboard/reports");
        }

        [HttpGet]
        [Route("Reports/View/{id}")]
        public async Task<ActionResult> All_Reports(string id)
        {
            GET_DATA();
            Report_Service.GetView_Report(long.Parse(id));
            return Redirect("~/Admin/Panel/Reports");
        }

        //Reports End












        //New Report Start
        [Route("NewReport")]
        public async Task<ActionResult> Reportss()
        {
            GET_DATA();
            ViewBag.Reports = Report_Service.All_Reports();
            return View("Dashboard/new_report");
        }

        [HttpPost]
        [Route("NewReport")]
        public async Task<ActionResult> Reportss(string title)
        {
            GET_DATA();
            Report_Service.Add_New_Report(title);
            return Redirect(Request.Headers.Referer);
        }

        [HttpGet]
        [Route("NewReport/Remove/{id}")]
        public async Task<ActionResult> Reports_Remove(string id)
        {
            GET_DATA();
            Report_Service.Remove_Report(long.Parse(id));
            return Redirect(Request.Headers.Referer);
        }
        //New Report End














        //Groups start
        [Route("Groups")]
        public async Task<ActionResult> Groups()
        {
            GET_DATA();
            ViewBag.Groups = Groups_Service.GetAllAccessTypeUser();
            return View("Dashboard/groups");
        }

        [HttpPost]
        [Route("Groups/New")]
        public async Task<ActionResult> NewGroups(string title,string en_title)
        {
            GET_DATA();
            Groups_Service.GetAddNewGroup(title, en_title);
            return Redirect(Request.Headers.Referer);
        }

        [HttpGet]
        [Route("Groups/Delete/{type_id}")]
        public async Task<ActionResult> RemoveGroups(string type_id)
        {
            GET_DATA();
            Groups_Service.GetRemoveAccessType(int.Parse(type_id));
            return Redirect(Request.Headers.Referer);
        }
        //Groups end












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















        //Settings start
        [Route("Settings")]
        public async Task<ActionResult> Settings()
        {
            GET_DATA();

            settings_tbl setting = Setting_Service.GetSettings();
            string parametrs = setting.parametrs;

            var parms = JObject.Parse(parametrs);
            ViewBag.logo = parms.GetValue("logo");
            ViewBag.law = parms.GetValue("law");
            ViewBag.slider = parms.GetValue("slider");
            ViewBag.banner = parms.GetValue("banner");

            return View("Dashboard/settings");
        }

        [HttpPost]
        [Route("Settings")]
        public async Task<ActionResult> Settings(String slider, String banner, String law, String icon)
        {
            JObject json = new JObject();
            json.Add("logo", icon);
            json.Add("law", law);
            json.Add("slider", slider);
            json.Add("banner", banner);

            Setting_Service.AddNewSetting(json.ToString(), "");

            return Redirect(Request.Headers.Referer.ToString());
        }
        //Settings end











        //Profile start
        [Route("Profile")]
        public async Task<ActionResult> Profile()
        {
            GET_DATA();
            Static_User.UserCity user = Static_User.GetUserByID(User_Data.id);
            users_tbl usr = user.user;
            ViewBag.USER = usr;
            ViewBag.City = user.city;
            return View("Dashboard/profile");
        }

        [HttpPost]
        [Route("Profile/Update")]
        public async Task<ActionResult> ProfileDone(Profile_Model profile,IFormFile profile_image)
        {

            if(profile_image != null)
            {
                profile.profile_image = profile_image;
            }

            GET_DATA();
            profile.id = User_Data.id;
            users_tbl user = Users_Service.UpdateUser(profile);
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
            GET_DATA();
            ViewBag.USER = Static_User.GetLogin_With_Token(User_Data.login_token);

            using (Database_DB DB = new Database_DB())
            {
                users_tbl user = (from Users_Tbl in DB.users_tbl where Users_Tbl.login_token.Equals(User_Data.login_token) select Users_Tbl).First();

                string secure = generateSecret();

                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                byte[] secure_byte = System.Text.Encoding.ASCII.GetBytes(secure);

                user.google_auth = secure;

                var qr_code = tfa.GenerateSetupCode("Sorapp", "Sorapp", secure_byte, 300, true);
                ViewBag.GoogleAuth2F = qr_code.QrCodeSetupImageUrl;

                DB.SaveChanges();

            }

            return View("Dashboard/profile2F");
        }

        [Route("Profile/Diactive2F")]
        public async Task<ActionResult> Diactive2F()
        {
            GET_DATA();
            using (Database_DB DB = new Database_DB())
            {
                users_tbl user = (from Users_Tbl in DB.users_tbl where Users_Tbl.login_token.Equals(User_Data.login_token) select Users_Tbl).First();
                user.google_auth = "";
                DB.SaveChanges();
            }

            return Redirect(Request.Headers.Referer.ToString());
        }
        //Profile end











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
        //My Functionss End


    }
}
