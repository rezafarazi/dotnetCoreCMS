using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OwncmsCore.Models.DatabaseModels;

namespace Own_Cms.Functions.Statics.City
{
    public class City
    {

        //Get All Cites Start
        public static List<Cites_Tbl> Get_All_Area()
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from Cites_Tbl in DB.Cites_Tbl where Cites_Tbl.area_id == "0" && Cites_Tbl.remove == false select Cites_Tbl).ToList();
            }
        }
        //Get All Cites End


        //Get New City Start
        public static void Add_New_City(string city_name,string area_id)
        {
            using (Database_DB DB = new Database_DB())
            {
                Cites_Tbl city = new Cites_Tbl();

                city.city_name = city_name;
                city.area_id = area_id;
                city.remove = false;

                DB.Cites_Tbl.Add(city);
                DB.SaveChanges();
            }
        }
        //Get New City End


        //Get All Cites And Area Start
        public static List<Cites_Tbl> Get_All_City_And_Area()
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from Cites_Tbl in DB.Cites_Tbl where Cites_Tbl.remove == false select Cites_Tbl).ToList();
            }
        }
        //Get All Cites And Area End


        //Get All Cites And Area Start
        public static void Get_Remove(long city_id)
        {
            using (Database_DB DB = new Database_DB())
            {
                Cites_Tbl city = (from Cites_Tbl in DB.Cites_Tbl where Cites_Tbl.id == city_id select Cites_Tbl).FirstOrDefault();
                city.remove = true;
                DB.SaveChanges();
            }
        }
        //Get All Cites And Area End


        //Get All Cites And Area Start
        public static Cites_Tbl Get_City(long city_id)
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from Cites_Tbl in DB.Cites_Tbl where Cites_Tbl.id == city_id select Cites_Tbl).FirstOrDefault();
            }
        }
        //Get All Cites And Area End


        //Get All Cites And Area Start
        public static void City_Edit(long city_id, string city_name, string area_id)
        {
            using (Database_DB DB = new Database_DB())
            {
                Cites_Tbl city = (from Cites_Tbl in DB.Cites_Tbl where Cites_Tbl.id == city_id select Cites_Tbl).FirstOrDefault();
                city.city_name = city_name;
                city.area_id = area_id;
                DB.SaveChanges();
            }
        }
        //Get All Cites And Area End

            
    }
}