using OwncmsCore.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Own_Cms.Functions.Statics.Services
{
    public class Services
    {


        public struct Service_Var
        {
            public List<Services_Tbl> service;
            public List<Categories_Tbl> category;
        }


        public struct User_Service_Var
        {
            public Services_Tbl Service;
            public Categories_Tbl Categoty;
            public Users_Tbl User;
        }


        public struct chart_count
        {
            public int all_services;
            public int current_services;
        }



        //Get All Services Start
        public static Service_Var Get_All_Not_Work_Services()
        {
            using (Database_DB DB = new Database_DB())
            {

                Service_Var services_var = new Service_Var();

                List<Services_Tbl> service=new List<Services_Tbl>();
                List<Categories_Tbl> category=new List<Categories_Tbl>();

                var services = (from Services_Tbl in DB.Services_Tbl
                                join Categories_Tbl in DB.Categories_Tbl on Services_Tbl.category.id equals Categories_Tbl.id
                                where Services_Tbl.status == "0"
                                select new { Services_Tbl , Categories_Tbl }).ToList();

                for (int i = 0; i < services.Count; i++)
                {
                    service.Add(services[i].Services_Tbl);
                    category.Add(services[i].Categories_Tbl);
                }

                services_var.service = service;
                services_var.category = category;

                return services_var;
            }
        }
        //Get All Services End




        //Get All Services Start
        public static Service_Var Get_All_Current_Work_Services()
        {
            using (Database_DB DB = new Database_DB())
            {

                Service_Var services_var = new Service_Var();

                List<Services_Tbl> service = new List<Services_Tbl>();
                List<Categories_Tbl> category = new List<Categories_Tbl>();

                var services = (from Services_Tbl in DB.Services_Tbl
                                join Categories_Tbl in DB.Categories_Tbl on Services_Tbl.category.id equals Categories_Tbl.id
                                where Services_Tbl.status == "1"
                                select new { Services_Tbl, Categories_Tbl }).ToList();

                for (int i = 0; i < services.Count; i++)
                {
                    service.Add(services[i].Services_Tbl);
                    category.Add(services[i].Categories_Tbl);
                }

                services_var.service = service;
                services_var.category = category;

                return services_var;
            }
        }
        //Get All Services End


        //Get All Services Start
        public static int Get_All_Count_Current_Work_Services()
        {
            using (Database_DB DB = new Database_DB())
            {
                
                var services = (from Services_Tbl in DB.Services_Tbl
                                join Categories_Tbl in DB.Categories_Tbl on Services_Tbl.category.id equals Categories_Tbl.id
                                where Services_Tbl.status == "1"
                                select new { Services_Tbl, Categories_Tbl }).ToList();

                return services.Count;
            }
        }
        //Get All Services End



        //Get All Services Start
        public static int Get_All_Count_All_Work_Services()
        {
            using (Database_DB DB = new Database_DB())
            {

                var services = (from Services_Tbl in DB.Services_Tbl
                                join Categories_Tbl in DB.Categories_Tbl on Services_Tbl.category.id equals Categories_Tbl.id
                                select new { Services_Tbl, Categories_Tbl }).ToList();

                return services.Count;
            }
        }
        //Get All Services End




        //Get All Data For Chart Start
        public static chart_count Get_Chart()
        {
            chart_count chart = new chart_count();

            chart.all_services = Get_All_Count_All_Work_Services();
            chart.current_services = Get_All_Count_Current_Work_Services();

            return chart;
        }
        //Get All Data For Chart End






        //Get All Services Start
        public static List<int> Get_Services_In_Week()
        {
            using (Database_DB DB = new Database_DB())
            {

                List<int> days = new List<int>();

                for (int i = 0; i < 8; i++)
                {
                    var services = (from Services_Tbl in DB.Services_Tbl
                                    where Services_Tbl.start_datetime == DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString()
                                    select Services_Tbl).ToList();
                    days.Add(services.Count);
                }

                return days;
            }
        }
        //Get All Services End



        //Get Service Start
        public static User_Service_Var Get_Service(long id)
        {
            using (Database_DB DB = new Database_DB())
            {

                User_Service_Var Service = new User_Service_Var();

                var services = (from Services_Tbl in DB.Services_Tbl
                                join Categories_Tbl in DB.Categories_Tbl on Services_Tbl.category.id equals Categories_Tbl.id
                                join Users_Tbl in DB.Users_Tbl on Services_Tbl.req_user.id equals Users_Tbl.id
                                where Services_Tbl.id == id
                                select new { Services_Tbl , Categories_Tbl , Users_Tbl}).FirstOrDefault();

                Service.Service = services.Services_Tbl;
                Service.Categoty = services.Categories_Tbl;
                Service.User = services.Users_Tbl;

                return Service;
            }
        }
        //Get Service End




        //Get Cancel Service Start
        public static void Get_Cancel_Service(long id)
        {
            using (Database_DB DB = new Database_DB())
            {

                User_Service_Var Service = new User_Service_Var();

                var services = (from Services_Tbl in DB.Services_Tbl
                                where Services_Tbl.id == id
                                select Services_Tbl).FirstOrDefault();
                services.status = "400";
                DB.SaveChanges();
            }
        }
        //Get Cancel Service End



    }
}