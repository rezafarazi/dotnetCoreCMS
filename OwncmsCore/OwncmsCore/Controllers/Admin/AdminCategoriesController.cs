using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Models.FormModels;
using OwncmsCore.Services.Panel.Categories;

namespace OwncmsCore.Controllers.Admin
{

    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminCategoriesController : Controller
    {

        //Get Cateogries Start
        [Route("Categoreis")]
        public async Task<ActionResult> Categoreis()
        {

            ViewBag.Categories = Categories_Service.GetAllCategories();
            return View("../Admin/Dashboard/categories");
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

            ViewBag.Categories = Categories_Service.GetAllCategories();
            ViewBag.Category = Categories_Service.GetCategory(long.Parse(category_id));
            return View("../Admin/Dashboard/categories");
        }
        //Get Categories End

    }
}
