using OwncmsCore.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Own_Cms.Functions.Statics.Reports
{
    public class Reports
    {

        //Get Add New Report Start
        public static void Add_New_Report(string title)
        {
            using (Database_DB DB = new Database_DB())
            {
                Reports_Tbl report = new Reports_Tbl();
                report.title = title;
                report.delete_flag = false;
                DB.Reports_Tbl.Add(report);
                DB.SaveChanges();
            }
        }
        //Get Add New Report End



        //Get Add New Report Start
        public static List<Reports_Tbl> All_Reports()
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from Reports_Tbl in DB.Reports_Tbl where Reports_Tbl.delete_flag==false select Reports_Tbl).ToList();
            }
        }
        //Get Add New Report End


        //Get Remove Report Start
        public static void Remove_Report(long report_id)
        {
            using (Database_DB DB = new Database_DB())
            {
                Reports_Tbl report = (from Reports_Tbl in DB.Reports_Tbl where Reports_Tbl.delete_flag == false && Reports_Tbl.id == report_id select Reports_Tbl).FirstOrDefault();
                report.delete_flag = true;
                DB.SaveChanges();
            }
        }
        //Get Remove Report End

    }
}