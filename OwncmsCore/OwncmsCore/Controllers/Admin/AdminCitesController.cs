using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Services.Panel.Cites;

namespace OwncmsCore.Controllers.Admin
{
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminCitesController : Controller
    {


        //Get Chart Start
        [Route("Cities")]
        public async Task<ActionResult> Cities()
        {

            return View("../Admin/Dashboard/cites");
        }

        [HttpGet]
        [Route("Cities/Edit/{city_id}")]
        public async Task<ActionResult> Cities(string city_id)
        {

            ViewBag.City_Data = City_Service.GetCity(long.Parse(city_id));
            return View("../Admin/Dashboard/cites");
        }

        [HttpPost]
        [Route("Cities/New")]
        public async Task<ActionResult> NewCity(string name, string area, string status)
        {
            City_Service.AddNewCityOrArea(name, int.Parse(area), status);
            return Redirect("/Admin/Panel/Cities");
        }

        [HttpPost]
        [Route("Cities/Update")]
        public async Task<ActionResult> UpdateCity(string id, string name, string area, string status)
        {
            City_Service.UpdateCityOrArea(long.Parse(id), name, int.Parse(area), status);
            return Redirect("/Admin/Panel/Cities");
        }
        //Get Chart End


    }
}
