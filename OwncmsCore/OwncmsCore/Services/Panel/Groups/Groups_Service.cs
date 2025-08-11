using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Groups
{
    public class Groups_Service
    {
        //Get all user types start
        public static List<userstype_tbl> GetAllAccessTypeUser()
        {
            using(Database_DB DB=new Database_DB())
            {
                return (from userstype_tbl in DB.userstype_tbl select userstype_tbl).ToList();
            }
        }
        //Get all user types end


        //Get add new group start
        public static void GetAddNewGroup(string title,string en_title)
        {
            using (Database_DB DB = new Database_DB())
            {
                userstype_tbl type = new userstype_tbl();

                type.title = title;
                type.en_title = en_title;

                DB.userstype_tbl.Add(type);
                DB.SaveChanges();
            }
        }
        //Get add new group end


        //Get remove user type start
        public static void GetRemoveAccessType(int type_id)
        {
            using (Database_DB DB = new Database_DB())
            {
                userstype_tbl type = (from userstype_tbl in DB.userstype_tbl select userstype_tbl).First();
                DB.userstype_tbl.Remove(type);
                DB.SaveChanges();
            }
        }
        //Get remove user type end

    }
}
