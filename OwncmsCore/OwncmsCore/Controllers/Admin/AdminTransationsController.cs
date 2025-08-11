using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Services.Panel.Transation;

namespace OwncmsCore.Controllers.Admin
{
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminTransationsController : Controller
    {



        //Transations start
        [Route("Transactions")]
        public async Task<ActionResult> Transations()
        {

            ViewBag.AllTransations = Transation_Service.GetAllTransations();
            return View("../Admin/Dashboard/transations");
        }
        //Transations end



    }
}
