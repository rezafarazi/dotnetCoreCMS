using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.AdminAuth;
using OwncmsCore.Middleware.Language;

namespace OwncmsCore.Controllers.Admin
{

    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminCategoriesController : Controller
    {



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
        }

        [HttpPost]
        [Route("Categoreis/New")]
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








    }
}
