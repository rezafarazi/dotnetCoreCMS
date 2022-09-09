using Newtonsoft.Json.Linq;
using OwncmsCore.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwncmsCore.Services.Global_Functions.Transation
{
    public class Transaction_fun
    {

        public void Get_Count_Down_Payment()
        {

            using (Database_DB DB=new Database_DB())
            {
                List<Users_Tbl> users = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.peyment_day_count != 0 && Users_Tbl.user_type == "WORKER_USER" select Users_Tbl).ToList();
                for (int i = 0; i < users.Count; i++)
                {
                    users[i].peyment_day_count = users[i].peyment_day_count - 1;
                }
                DB.SaveChanges();
            }

        }



        public void Get_Count_Up_Payment()
        {

            using (Database_DB DB = new Database_DB())
            {
                List<Users_Tbl> users = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.peyment_day_count != 0 && Users_Tbl.user_type == "WORKER_USER" select Users_Tbl).ToList();
                for (int i = 0; i < users.Count; i++)
                {
                    users[i].peyment_day_count = users[i].peyment_day_count + 1;
                }
                DB.SaveChanges();
            }

        }

    }
}