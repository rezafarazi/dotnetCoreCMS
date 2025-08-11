using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.Language;
<<<<<<< HEAD

namespace OwncmsCore.Controllers.Admin
{
=======
using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Controllers.Admin
{


>>>>>>> master
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminDashController : Controller
    {

<<<<<<< HEAD

        //Get Dashboard Start
        [Route("Dashboard")]
        public async Task<ActionResult> Dashboard()
        {
=======
        //Get Dashboard Start
        [Route("Dashboard")]
        public async Task<ActionResult> Index()
        {
            ViewBag.User_Data = HttpContext.Items["User_Data"];
            ViewBag.Notification = HttpContext.Items["Notification"];


>>>>>>> master
            return View("../Admin/Dashboard/index");
        }
        //Get Dashboard End


<<<<<<< HEAD
        //Get Chart Start
        [Route("Charts")]
        public async Task<ActionResult> Chart()
        {

            return View("../Admin/Dashboard/chart");
        }
        //Get Chart End

=======
        //Chart Start
        [Route("Charts")]
        public async Task<ActionResult> Charts()
        {

            return View("../Admin/Dashboard/charts");
        }
        //Chart End
>>>>>>> master

    }
}
