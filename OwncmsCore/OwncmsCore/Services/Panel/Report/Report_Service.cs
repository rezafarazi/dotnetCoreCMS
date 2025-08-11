using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Report
{
    public class Report_Service
    {


        //Get Add New Report Start
        public static void Add_New_Report(string title)
        {
            using (Database_DB DB = new Database_DB())
            {
                report_tbl report = new report_tbl();
                report.title = title;
                report.delete_flag = false;
                DB.report_tbl.Add(report);
                DB.SaveChanges();
            }
        }
        //Get Add New Report End



        //Get Add New Report Start
        public static List<report_tbl> All_Reports()
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from Reports_Tbl in DB.report_tbl where Reports_Tbl.delete_flag == false select Reports_Tbl).ToList();
            }
        }
        //Get Add New Report End


        //Get Remove Report Start
        public static void Remove_Report(long report_id)
        {
            using (Database_DB DB = new Database_DB())
            {
                report_tbl report = (from Reports_Tbl in DB.report_tbl where Reports_Tbl.delete_flag == false && Reports_Tbl.id == report_id select Reports_Tbl).FirstOrDefault();
                report.delete_flag = true;
                DB.SaveChanges();
            }
        }
        //Get Remove Report End













        //Get All Reports Strct
        public struct Reports_Var
        {
            public List<reports_tbl> AllReports;
            public List<report_tbl> ReportType;
        }



        //Get All Reports Start
        public static Reports_Var GetAll_User_Reports()
        {
            using (Database_DB DB = new Database_DB())
            {
                Reports_Var reports = new Reports_Var();

                List<reports_tbl> AllReports = new List<reports_tbl>();
                List<report_tbl> ReportType = new List<report_tbl>();



                var All_Data = (from reports_tbl in DB.reports_tbl
                                join report_tbl in DB.report_tbl on reports_tbl.report_id.id equals report_tbl.id
                                where reports_tbl.condition == false
                                select new { reports = reports_tbl, report = report_tbl }).ToList();

                for (int i = 0; i < All_Data.Count; i++)
                {
                    AllReports.Add(All_Data[i].reports);
                    ReportType.Add(All_Data[i].report);
                }

                reports.AllReports = AllReports;
                reports.ReportType = ReportType;

                return reports;
            }
        }
        //Get All Reports End


        //Get Seen Report Start
        public static void GetView_Report(long Report_Id)
        {
            using (Database_DB DB = new Database_DB())
            {
                reports_tbl report = (from UsersReports_Tbl in DB.reports_tbl where UsersReports_Tbl.id == Report_Id select UsersReports_Tbl).FirstOrDefault();
                report.condition = true;
                DB.SaveChanges();
            }
        }
        //Get Seen Report End






    }
}
