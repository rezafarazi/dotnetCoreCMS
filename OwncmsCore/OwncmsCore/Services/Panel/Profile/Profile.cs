using System.Linq;
using OwncmsCore.Models.DatabaseModels;

namespace Services.Panel.Profile
{
    public class Profile
    {

        //Global Variables Start
        public struct GET_USER_PROFILE_Stcuct
        {
            public Users_Tbl User;
        }
        //Global Variables End


        public static GET_USER_PROFILE_Stcuct GET_USER_PROFILE_BY_ID(long u_id)
        {
            GET_USER_PROFILE_Stcuct GET_USER_PROFILE = new GET_USER_PROFILE_Stcuct();

            using (Database_DB DB = new Database_DB())
            {
                GET_USER_PROFILE.User = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id == u_id select Users_Tbl).SingleOrDefault();
            }

            return GET_USER_PROFILE;

        }
        

    }
}