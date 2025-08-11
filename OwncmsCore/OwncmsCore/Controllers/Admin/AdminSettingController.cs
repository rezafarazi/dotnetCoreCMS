using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Services.Panel.Settings;

namespace OwncmsCore.Controllers.Admin
{
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminSettingController : Controller
    {

        //Settings start
        [Route("Settings")]
        public async Task<ActionResult> Settings()
        {


            settings_tbl setting = Setting_Service.GetSettings();
            string parametrs = setting.parametrs;

            var parms = JObject.Parse(parametrs);
            ViewBag.logo = parms.GetValue("logo");
            ViewBag.law = parms.GetValue("law");
            ViewBag.slider = parms.GetValue("slider");
            ViewBag.banner = parms.GetValue("banner");

            return View("../Admin/Dashboard/settings");
        }

        [HttpPost]
        [Route("Settings")]
        public async Task<ActionResult> Settings(String slider, String banner, String law, String icon)
        {
            JObject json = new JObject();
            json.Add("logo", icon);
            json.Add("law", law);
            json.Add("slider", slider);
            json.Add("banner", banner);

            Setting_Service.AddNewSetting(json.ToString(), "");

            return Redirect(Request.Headers.Referer.ToString());
        }
        //Settings end


    }
}
