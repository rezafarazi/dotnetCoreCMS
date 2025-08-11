using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Cites
{
    public class City_Service
    {


        //Get city start
        public static city_tbls GetCity(long city_id)
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from city_tbls in DB.city_tbls where city_tbls.id == city_id select city_tbls).First();
            }
        }
        //Get city end


        //Get all cites and area start
        public static List<city_tbls> GetAllCitesAndArea()
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from city_tbls in DB.city_tbls select city_tbls).ToList();
            }
        }
        //Get all cites and area end


        //Get all cites start
        public static List<city_tbls> GetAllActiveCites()
        {
            using(Database_DB DB=new Database_DB())
            {
                return (from city_tbls in DB.city_tbls where city_tbls.status=="1" && city_tbls.area != 0 select city_tbls).ToList();
            }
        }
        //Get all cites end


        //Get all area start
        public static List<city_tbls> GetAllArea()
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from city_tbls in DB.city_tbls where city_tbls.area == 0 select city_tbls).ToList();
            }
        }
        //Get all area end


        //Add new city or area start
        public static void AddNewCityOrArea(string name,int area,string status)
        {
            using(Database_DB DB=new Database_DB())
            {
                city_tbls city = new city_tbls() {
                    name=name,
                    area=area,
                    datetime=DateTime.Now.ToString(),
                    status=status
                };

                DB.city_tbls.Add(city);
                DB.SaveChanges();
            }
        }
        //Add new city or area end


        //Update city or area start
        public static void UpdateCityOrArea(long id,string name, int area, string status)
        {
            using (Database_DB DB = new Database_DB())
            {
                city_tbls city = (from city_tbls in DB.city_tbls where city_tbls.id == id select city_tbls).First();

                city.name = name;
                city.area = area;
                city.status = status;

                DB.SaveChanges();
            }
        }
        //Update city or area end

    }
}
