using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.AdminAuth;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Services.Panel.Categories;
using OwncmsCore.Services.Panel.Post;
using static OwncmsCore.Services.Panel.Post.Post_Service;

namespace OwncmsCore.Controllers.Admin
{
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminPostController : Controller
    {

        //New Post Start

        [Route("NewPost")]
        public async Task<ActionResult> NewPost()
        {

            ViewBag.AllCategories = Categories_Service.GetAllCategories();
            return View("../Admin/Dashboard/newpost");
        }

        [Route("NewPostDone")]
        public async Task<ActionResult> AddNewPost(string title, string category, string visible, string img_adrs, string cntent)
        {

            Post_Service.Insert(title, cntent, img_adrs, (visible == "1") ? true : false, AdminAuth.UserData.id, long.Parse(category));
            return Redirect(Request.Headers.Referer.ToString());
        }

        //New Post End




        //AllPosts Start

        [Route("AllPosts")]
        public async Task<ActionResult> AllPosts()
        {

            ViewBag.AllPosts = Post_Service.Select();
            return View("../Admin/Dashboard/allposts");
        }


        [Route("AllPosts/{id}")]
        public async Task<ActionResult> GetPost(string id)
        {

            PostData_struc post = Post_Service.Select(long.Parse(id));
            ViewBag.AllCategories = Categories_Service.GetAllCategories();
            ViewBag.Post = post.Post;
            ViewBag.Cat = post.Category;
            return View("../Admin/Dashboard/editpost");
        }


        [Route("AllPosts/Update")]
        public async Task<ActionResult> PostUpdate(string id, string title, string category, string visible, string img_adrs, string cntent)
        {

            Post_Service.Update(long.Parse(id), title, cntent, img_adrs, (visible == "1") ? true : false, AdminAuth.UserData.id, long.Parse(category));
            return View("../Admin/Dashboard/done");
        }


        [Route("AllPosts/Remove/{id}")]
        public async Task<ActionResult> PostRemove(string id)
        {

            Post_Service.Delete(long.Parse(id));
            return Redirect(Request.Headers.Referer.ToString());
        }

        //AllPosts End


    }
}
