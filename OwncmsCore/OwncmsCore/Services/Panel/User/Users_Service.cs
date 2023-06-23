using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Models.FormModels;
using OwncmsCore.Services.File;

namespace OwncmsCore.Services.Panel.User
{
    public class Users_Service
    {

        //Update user start
        public static users_tbl UpdateUser(Profile_Model profile)
        {
            using(Database_DB DB=new Database_DB())
            {

                long city_id = long.Parse(profile.city);

                users_tbl user = (from users_tbl in DB.users_tbl where users_tbl.id==profile.id select users_tbl).First();
                city_tbls city = (from city_tbls in DB.city_tbls where city_tbls.id == city_id select city_tbls).First();

                user.username = profile.username;
                user.name = (profile.name==null)?"":profile.name;
                user.family = (profile.family==null)?"":profile.family;
                user.address = (profile.address==null)?"":profile.address;
                user.bio = (profile.bio==null)?"": profile.bio;
                user.phone = (profile.phone==null)?"": profile.phone;
                user.email = (profile.email==null)?"": profile.email;
                user.city_id = city;

                if (!String.IsNullOrEmpty(profile.user_type))
                {
                    user.user_type = profile.user_type;
                }

                if (!String.IsNullOrEmpty(profile.password))
                {
                    
                    user.password = Hasher_Lib.Hash.SHA_256(profile.password);
                }

                if (profile.profile_image != null)
                {
                    file_tbl profile_image = File_Service.NewFile(profile.profile_image);
                    user.profile_image_file_id = profile_image;
                }


                DB.SaveChanges();

                return user;
            }
        }
        //Update user end

    }
}
