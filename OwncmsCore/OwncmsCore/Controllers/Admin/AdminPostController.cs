using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.AdminAuth;
using OwncmsCore.Middleware.Language;
<<<<<<< HEAD
using OwncmsCore.Services.Panel.Categories;
using OwncmsCore.Services.Panel.Post;
using static OwncmsCore.Services.Panel.Post.Post_Service;

namespace OwncmsCore.Controllers.Admin
{
=======
using OwncmsCore.Services.Panel.Posts;
using static OwncmsCore.Services.Panel.Posts.Posts;

namespace OwncmsCore.Controllers.Admin
{

>>>>>>> master
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminPostController : Controller
    {

<<<<<<< HEAD
=======

>>>>>>> master
        //New Post Start

        [Route("NewPost")]
        public async Task<ActionResult> NewPost()
        {

<<<<<<< HEAD
            ViewBag.AllCategories = Categories_Service.GetAllCategories();
=======
            ViewBag.AllCategories = OwncmsCore.Services.Panel.Categories.Categories.Select();
>>>>>>> master
            return View("../Admin/Dashboard/newpost");
        }

        [Route("NewPostDone")]
        public async Task<ActionResult> AddNewPost(string title, string category, string visible, string img_adrs, string cntent)
        {

<<<<<<< HEAD
            Post_Service.Insert(title, cntent, img_adrs, (visible == "1") ? true : false, AdminAuth.UserData.id, long.Parse(category));
=======
            Posts.Insert(title, cntent, img_adrs, (visible == "1") ? true : false, AdminAuth.AdminUser.id, long.Parse(category));
>>>>>>> master
            return Redirect(Request.Headers.Referer.ToString());
        }

        //New Post End




<<<<<<< HEAD
=======


>>>>>>> master
        //AllPosts Start

        [Route("AllPosts")]
        public async Task<ActionResult> AllPosts()
        {

<<<<<<< HEAD
            ViewBag.AllPosts = Post_Service.Select();
=======
            ViewBag.AllPosts = Posts.Select();
>>>>>>> master
            return View("../Admin/Dashboard/allposts");
        }


        [Route("AllPosts/{id}")]
        public async Task<ActionResult> GetPost(string id)
        {

<<<<<<< HEAD
            PostData_struc post = Post_Service.Select(long.Parse(id));
            ViewBag.AllCategories = Categories_Service.GetAllCategories();
=======
            PostData_struc post = Posts.Select(long.Parse(id));
            ViewBag.AllCategories = OwncmsCore.Services.Panel.Categories.Categories.Select();
>>>>>>> master
            ViewBag.Post = post.Post;
            ViewBag.Cat = post.Category;
            return View("../Admin/Dashboard/editpost");
        }


        [Route("AllPosts/Update")]
        public async Task<ActionResult> PostUpdate(string id, string title, string category, string visible, string img_adrs, string cntent)
        {

<<<<<<< HEAD
            Post_Service.Update(long.Parse(id), title, cntent, img_adrs, (visible == "1") ? true : false, AdminAuth.UserData.id, long.Parse(category));
=======
            Posts.Update(long.Parse(id), title, cntent, img_adrs, (visible == "1") ? true : false, AdminAuth.AdminUser.id, long.Parse(category));
>>>>>>> master
            return View("../Admin/Dashboard/done");
        }


        [Route("AllPosts/Remove/{id}")]
        public async Task<ActionResult> PostRemove(string id)
        {

<<<<<<< HEAD
            Post_Service.Delete(long.Parse(id));
=======
            Posts.Delete(long.Parse(id));
>>>>>>> master
            return Redirect(Request.Headers.Referer.ToString());
        }

        //AllPosts End


    }
}
