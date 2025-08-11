using Microsoft.AspNetCore.Mvc;
using OwncmsCore.Middleware.Language;
<<<<<<< HEAD
using OwncmsCore.Services.Panel.Report;

namespace OwncmsCore.Controllers.Admin
{
=======
using OwncmsCore.Services.Panel.Reports;
using OwncmsCore.Services.Panel.UsersReports;

namespace OwncmsCore.Controllers.Admin
{

>>>>>>> master
    [Route("Admin/Panel")]
    [LanguageMiddle]
    public class AdminReportController : Controller
    {

<<<<<<< HEAD
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



=======
>>>>>>> master
        //New Report Start
        [Route("NewReport")]
        public async Task<ActionResult> Reportss()
        {

<<<<<<< HEAD
            ViewBag.Reports = Report_Service.All_Reports();
=======
            ViewBag.Reports = Reports.All_Reports();
>>>>>>> master
            return View("../Admin/Dashboard/new_report");
        }

        [HttpPost]
        [Route("NewReport")]
        public async Task<ActionResult> Reportss(string title)
        {

<<<<<<< HEAD
            Report_Service.Add_New_Report(title);
=======
            Reports.Add_New_Report(title);
>>>>>>> master
            return Redirect(Request.Headers.Referer);
        }

        [HttpGet]
        [Route("NewReport/Remove/{id}")]
        public async Task<ActionResult> Reports_Remove(string id)
        {

<<<<<<< HEAD
            Report_Service.Remove_Report(long.Parse(id));
=======
            Reports.Remove_Report(long.Parse(id));
>>>>>>> master
            return Redirect(Request.Headers.Referer);
        }
        //New Report End



<<<<<<< HEAD
=======


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


>>>>>>> master
    }
}
