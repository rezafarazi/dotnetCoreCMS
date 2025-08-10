using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.Language;
using OwncmsCore.Services.Panel.Reports;
using OwncmsCore.Services.Panel.UsersReports;

namespace OwncmsCore.Controllers.Admin
{

    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminReportController : Controller
    {

        //New Report Start
        [Route("NewReport")]
        public async Task<ActionResult> Reportss()
        {

            ViewBag.Reports = Reports.All_Reports();
            return View("../Admin/Dashboard/new_report");
        }

        [HttpPost]
        [Route("NewReport")]
        public async Task<ActionResult> Reportss(string title)
        {

            Reports.Add_New_Report(title);
            return Redirect(Request.Headers.Referer);
        }

        [HttpGet]
        [Route("NewReport/Remove/{id}")]
        public async Task<ActionResult> Reports_Remove(string id)
        {

            Reports.Remove_Report(long.Parse(id));
            return Redirect(Request.Headers.Referer);
        }
        //New Report End





        //Reports Start

        [Route("Reports")]
        public async Task<ActionResult> All_Reports()
        {

            ViewBag.AllReports = UsersReports.GetAll_User_Reports();
            return View("../Admin/Dashboard/reports");
        }

        [HttpGet]
        [Route("Reports/View/{id}")]
        public async Task<ActionResult> All_Reports(string id)
        {

            UsersReports.GetView_Report(long.Parse(id));
            return Redirect("~/Admin/Panel/Reports");
        }

        //Reports End


    }
}
