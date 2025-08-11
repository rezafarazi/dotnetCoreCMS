using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Models.FormModels;
using OwncmsCore.Services.Panel.Categories;

namespace OwncmsCore.Controllers.Admin.Shop
{
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminShopCategoiresController : Controller
    {


        /**************************************Shop****************************************/

        //Get Cateogries Start
        [Route("Store/CategoryStore")]
        public async Task<ActionResult> StoreCategoreis()
        {

            ViewBag.Categories = Store_Categories_Service.GetAllCategories();
            return View("../Admin/Dashboard/store_categories");
        }

        [HttpPost]
        [Route("Store/CategoryStore/New")]
        public async Task<ActionResult> StoreNewCategory(Category_Model category)
        {
            Store_Categories_Service.NewCategory(category);
            return Redirect("~/Admin/Panel/Store/CategoryStore");
        }

        [HttpPost]
        [Route("Store/CategoryStore/UpdateDone")]
        public async Task<ActionResult> StoreUpdateCategoryDone(Category_Model category)
        {
            Store_Categories_Service.UpdateCategory(category);
            return Redirect("~/Admin/Panel/Store/CategoryStore");
        }

        [HttpGet]
        [Route("Store/CategoryStore/Remove/{category_id}")]
        public async Task<ActionResult> StoreDeleteCategory(string category_id)
        {
            Store_Categories_Service.DeleteCategory(long.Parse(category_id));
            return Redirect(Request.Headers.Referer.ToString());
        }

        [HttpGet]
        [Route("Store/CategoryStore/Update/{category_id}")]
        public async Task<ActionResult> StoreUpdateCategory(string category_id)
        {

            ViewBag.Categories = Store_Categories_Service.GetAllCategories();
            ViewBag.Category = Store_Categories_Service.GetCategory(long.Parse(category_id));
            return View("../Admin/Dashboard/store_categories");
        }
        //Get Categories End


    }
}
