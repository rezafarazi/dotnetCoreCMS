using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Categories
{
    public class Categories
    {

        //Get Insert New Category Start
        public static void Insert(string title,string text,string link,string icon_address,string type,string enable_flag,string root_id, Models.DatabaseModels.Users_Tbl user)
        {
            using(Models.DatabaseModels.Database_DB DB=new Models.DatabaseModels.Database_DB())
            {

                Users_Tbl usr = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id.Equals(user.id) select Users_Tbl).First();

                Categories_Tbl category = new Categories_Tbl();

                category.title = title;
                category.Icon_Address = icon_address;
                category.Type = type;
                category.Create_Date = PersianDateTime.Now.ToString();
                category.Enabled_Flag = enable_flag;
                category.Root_Id = root_id;
                category.User_Id = usr;
                category.link = link;
                category.text = text;

                DB.Categories_Tbl.Add(category);
                DB.SaveChanges();
            }
        }
        //Get Insert New Category End



        //Get Update Category Start
        public static void Update(long Cat_id,string title, string text, string link, string icon_address, string type, string enable_flag, string root_id, Models.DatabaseModels.Users_Tbl user)
        {
            using (Models.DatabaseModels.Database_DB DB = new Models.DatabaseModels.Database_DB())
            {

                Users_Tbl usr = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id.Equals(user.id) select Users_Tbl).First();

                Categories_Tbl category = (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.id==Cat_id select Categories_Tbl).FirstOrDefault();

                category.title = title;
                category.Icon_Address = icon_address;
                category.Type = type;
                category.Create_Date = PersianDateTime.Now.ToString();
                category.Enabled_Flag = enable_flag;
                category.Root_Id = root_id;
                category.User_Id = usr;
                category.link = link;
                category.text = text;

                DB.SaveChanges();
            }
        }
        //Get Update Category End



        //Get All Categories Start
        public static List<Categories_Tbl> Select()
        {
            using (Models.DatabaseModels.Database_DB DB = new Models.DatabaseModels.Database_DB())
            {
                return (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.Enabled_Flag!="DELETE" select Categories_Tbl).ToList();
            }
        }
        //Get All Categories End



        //Get All Categories Start
        public static Categories_Tbl Select(long id)
        {
            using (Models.DatabaseModels.Database_DB DB = new Models.DatabaseModels.Database_DB())
            {
                return (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.Enabled_Flag != "DELETE" && Categories_Tbl.id==id select Categories_Tbl).First();
            }
        }
        //Get All Categories End



        //Get Remove Category Start
        public static void Delete(long id)
        {
            using(Database_DB DB=new Database_DB())
            {
                Categories_Tbl category = (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.Enabled_Flag != "DELETE" select Categories_Tbl).First();
                category.Enabled_Flag = "DELETE";
                DB.SaveChanges();
            }
        }
        //Get Remove Category End


    }
}
