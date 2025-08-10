using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Controllers.Admin
{


    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminDashController : Controller
    {

        //Get Dashboard Start
        [Route("Dashboard")]
        public async Task<ActionResult> Index()
        {
            ViewBag.User_Data = HttpContext.Items["User_Data"];
            ViewBag.Notification = HttpContext.Items["Notification"];


            return View("../Admin/Dashboard/index");
        }
        //Get Dashboard End


        //Chart Start
        [Route("Charts")]
        public async Task<ActionResult> Charts()
        {

            return View("../Admin/Dashboard/charts");
        }
        //Chart End

    }
}
