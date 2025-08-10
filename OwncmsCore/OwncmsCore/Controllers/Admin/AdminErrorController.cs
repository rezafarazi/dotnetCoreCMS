using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.Language;

namespace OwncmsCore.Controllers.Admin
{

    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminErrorController : Controller
    {

        //Get 404 Start
        [HttpGet]
        [Route("")]
        public void Not_Found()
        {

            Response.Redirect("/Admin/Login");
        }
        //Get 404 End

    }
}
