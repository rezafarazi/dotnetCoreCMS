using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Transation
{
    public class Transation_Service
    {


        //Get all transations start
        public static List<Tuple<transcation_tbl,users_tbl>> GetAllTransations()
        {
            using(Database_DB DB=new Database_DB())
            {
                var resource = (from transcation_tbl in DB.transcation_tbl 
                              join users_tbl in DB.users_tbl on transcation_tbl.user_id.id equals users_tbl.id
                              select new { trans=transcation_tbl , user=users_tbl }).ToList();

                List<Tuple<transcation_tbl, users_tbl>> result = new List<Tuple<transcation_tbl, users_tbl>>();

                for (int i = 0; i < resource.Count(); i++)
                {
                    result.Add(new Tuple<transcation_tbl, users_tbl>(resource[i].trans, resource[i].user));
                }

                return result;

            }
        }
        //Get all transations end


    }
}
