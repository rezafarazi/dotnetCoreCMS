using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.AdminAuth;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Models.FormModels;
using OwncmsCore.Services.Panel.Categories;
using OwncmsCore.Services.Panel.Static_User;
using OwncmsCore.Services.Panel.Store;

namespace OwncmsCore.Controllers.Admin.Shop
{
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminShopPostController : Controller
    {



        //New store post start
        [Route("Store/NewStore")]
        public async Task<ActionResult> NewStorePost()
        {

            ViewBag.AllCategories = Store_Categories_Service.GetAllCategories();
            return View("../Admin/Dashboard/store_new");
        }

        [HttpPost]
        [Route("Store/NewStore/New")]
        public async Task<ActionResult> NewStorePostDone(NewStore_Model model)
        {

            Static_User.UserCity user = Static_User.GetUserByID(AdminAuth.UserData.id);
            users_tbl usr = user.user;
            Store_Service.AddNewStoreItem(usr, model);
            return Redirect(Request.Headers.Referer.ToString());
        }
        //New store post end




    }
}
