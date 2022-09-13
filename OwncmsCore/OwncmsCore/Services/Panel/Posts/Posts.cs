using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Posts
{
    public class Posts
    {

        //Add New Post Start
        public static void Insert(string title,string content,string image,bool visible,long user_id,long cat_id)
        {
            using(Database_DB DB=new Database_DB())
            {

                Users_Tbl usr_id = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id==user_id select Users_Tbl).FirstOrDefault();
                Categories_Tbl categ = (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.id == cat_id select Categories_Tbl).FirstOrDefault();

                Posts_Tbl post = new Posts_Tbl();

                post.title = title;
                post.content = content;
                post.image = image;
                post.visible = visible;
                post.delete_flag = false;
                post.date = PersianDateTime.Now.ToString();
                post.User_Id = usr_id;
                post.category_id = categ;

                DB.Posts_Tbl.Add(post);
                DB.SaveChanges();
            }
        }
        //Add New Post End

    }
}
