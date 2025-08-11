using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Services.Panel.Report;

namespace OwncmsCore.Controllers.Admin
{
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminReportController : Controller
    {

        //Reports Start

        [Route("Reports")]
        public async Task<ActionResult> All_Reports()
        {

            ViewBag.AllReports = Report_Service.GetAll_User_Reports();
            return View("../Admin/Dashboard/reports");
        }

        [HttpGet]
        [Route("Reports/View/{id}")]
        public async Task<ActionResult> All_Reports(string id)
        {

            Report_Service.GetView_Report(long.Parse(id));
            return Redirect("~/Admin/Panel/Reports");
        }

        //Reports End



        //New Report Start
        [Route("NewReport")]
        public async Task<ActionResult> Reportss()
        {

            ViewBag.Reports = Report_Service.All_Reports();
            return View("../Admin/Dashboard/new_report");
        }

        [HttpPost]
        [Route("NewReport")]
        public async Task<ActionResult> Reportss(string title)
        {

            Report_Service.Add_New_Report(title);
            return Redirect(Request.Headers.Referer);
        }

        [HttpGet]
        [Route("NewReport/Remove/{id}")]
        public async Task<ActionResult> Reports_Remove(string id)
        {

            Report_Service.Remove_Report(long.Parse(id));
            return Redirect(Request.Headers.Referer);
        }
        //New Report End



    }
}
