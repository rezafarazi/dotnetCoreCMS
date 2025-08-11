using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using OwncmsCore.Middleware.Language;
using OwncmsCore.Models.FormModels;
using OwncmsCore.Services.Panel.Categories;
=======
using OwncmsCore.Middleware.AdminAuth;
using OwncmsCore.Middleware.Language;
>>>>>>> master

namespace OwncmsCore.Controllers.Admin
{

    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminCategoriesController : Controller
    {

<<<<<<< HEAD
        //Get Cateogries Start
        [Route("Categoreis")]
        public async Task<ActionResult> Categoreis()
        {

            ViewBag.Categories = Categories_Service.GetAllCategories();
            return View("../Admin/Dashboard/categories");
=======


        //Categories Start
        [Route("Categoreis")]
        public async Task<ActionResult> Categories([FromQuery] string Cat_Id)
        {

            ViewBag.AllCategories = OwncmsCore.Services.Panel.Categories.Categories.Select();

            if (!String.IsNullOrEmpty(Cat_Id))
            {
                ViewBag.EditCategory = OwncmsCore.Services.Panel.Categories.Categories.Select(long.Parse(Cat_Id));
                return View("../Admin/Dashboard/editcategoreis");
            }
            else
            {
                return View("../Admin/Dashboard/categoreis");
            }
>>>>>>> master
        }

        [HttpPost]
        [Route("Categoreis/New")]
<<<<<<< HEAD
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
=======
        public void New_Category(string Category_Id, string Category_Name, string Icon_Address, string Root_Cat, string Visibleity, string link, string TextData)
        {
            OwncmsCore.Services.Panel.Categories.Categories.Insert(Category_Name, TextData, link, Icon_Address, (Root_Cat == "0") ? "MAIN" : "SUB", Visibleity, Root_Cat, AdminAuth.AdminUser);
            Response.Redirect("/Admin/Panel/Categoreis");
        }

        [HttpPost]
        [Route("Categoreis/Update")]
        public void Update_Category(string Category_Id, string Category_Name, string Icon_Address, string Root_Cat, string Visibleity, string link, string TextData)
        {


            OwncmsCore.Services.Panel.Categories.Categories.Update(long.Parse(Category_Id), Category_Name, TextData, link, Icon_Address, (Root_Cat == "0") ? "MAIN" : "SUB", Visibleity, Root_Cat, AdminAuth.AdminUser);
            Response.Redirect("/Admin/Panel/Categoreis");
        }

        [Route("Categoreis/Delete")]
        public void Delete_Category([FromQuery] string Delete)
        {

            OwncmsCore.Services.Panel.Categories.Categories.Delete(long.Parse(Delete));
            Response.Redirect("/Admin/Panel/Categoreis");
        }
        //Categories End







>>>>>>> master

    }
}
