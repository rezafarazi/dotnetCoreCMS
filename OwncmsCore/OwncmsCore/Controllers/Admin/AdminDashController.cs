using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.Language;

namespace OwncmsCore.Controllers.Admin
{
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminDashController : Controller
    {


        //Get Dashboard Start
        [Route("Dashboard")]
        public async Task<ActionResult> Dashboard()
        {
            return View("../Admin/Dashboard/index");
        }
        //Get Dashboard End


        //Get Chart Start
        [Route("Charts")]
        public async Task<ActionResult> Chart()
        {

            return View("../Admin/Dashboard/chart");
        }
        //Get Chart End


    }
}
