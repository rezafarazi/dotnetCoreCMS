using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.AdminAuth;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Services.Panel.Posts;
using static OwncmsCore.Services.Panel.Posts.Posts;

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

            ViewBag.AllCategories = OwncmsCore.Services.Panel.Categories.Categories.Select();
            return View("../Admin/Dashboard/newpost");
        }

        [Route("NewPostDone")]
        public async Task<ActionResult> AddNewPost(string title, string category, string visible, string img_adrs, string cntent)
        {

            Posts.Insert(title, cntent, img_adrs, (visible == "1") ? true : false, AdminAuth.AdminUser.id, long.Parse(category));
            return Redirect(Request.Headers.Referer.ToString());
        }

        //New Post End






        //AllPosts Start

        [Route("AllPosts")]
        public async Task<ActionResult> AllPosts()
        {

            ViewBag.AllPosts = Posts.Select();
            return View("../Admin/Dashboard/allposts");
        }


        [Route("AllPosts/{id}")]
        public async Task<ActionResult> GetPost(string id)
        {

            PostData_struc post = Posts.Select(long.Parse(id));
            ViewBag.AllCategories = OwncmsCore.Services.Panel.Categories.Categories.Select();
            ViewBag.Post = post.Post;
            ViewBag.Cat = post.Category;
            return View("../Admin/Dashboard/editpost");
        }


        [Route("AllPosts/Update")]
        public async Task<ActionResult> PostUpdate(string id, string title, string category, string visible, string img_adrs, string cntent)
        {

            Posts.Update(long.Parse(id), title, cntent, img_adrs, (visible == "1") ? true : false, AdminAuth.AdminUser.id, long.Parse(category));
            return View("../Admin/Dashboard/done");
        }


        [Route("AllPosts/Remove/{id}")]
        public async Task<ActionResult> PostRemove(string id)
        {

            Posts.Delete(long.Parse(id));
            return Redirect(Request.Headers.Referer.ToString());
        }

        //AllPosts End


    }
}
