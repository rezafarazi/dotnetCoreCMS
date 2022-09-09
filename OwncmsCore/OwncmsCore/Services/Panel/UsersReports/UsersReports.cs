using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Own_Cms.Functions.Statics.UsersReports
{
    public class UsersReports
    {


        //Get All Reports Strct
        public struct Reports_Var
        {
            public List<UsersReports_Tbl> AllReports;
            public List<Reports_Tbl> ReportType;
        }



        //Get All Reports Start
        public static Reports_Var GetAll_User_Reports()
        {
            using (Database_DB DB = new Database_DB())
            {
                Reports_Var reports = new Reports_Var();

                List<UsersReports_Tbl> AllReports=new List<UsersReports_Tbl>();
                List<Reports_Tbl> ReportType=new List<Reports_Tbl>();

                var All_Data=(from UsersReports_Tbl in DB.UsersReports_Tbl
                             join Reports_Tbl in DB.Reports_Tbl on UsersReports_Tbl.Report_Type.id equals Reports_Tbl.id
                             where UsersReports_Tbl.Seen == false select new { reports = UsersReports_Tbl , report = Reports_Tbl }).ToList();

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
                UsersReports_Tbl report = (from UsersReports_Tbl in DB.UsersReports_Tbl where UsersReports_Tbl.id==Report_Id select UsersReports_Tbl).FirstOrDefault();
                report.Seen = true;
                DB.SaveChanges();
            }
        }
        //Get Seen Report End

    }
}