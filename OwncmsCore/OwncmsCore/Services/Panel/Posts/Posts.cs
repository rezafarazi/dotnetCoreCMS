using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Posts
{
    public class Posts
    {


        public struct PostData_struc
        {
            public Posts_Tbl Post;
            public Categories_Tbl Category;
        }



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



        //Get All Posts Start
        public static List<Posts_Tbl> Select()
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from Posts_Tbl in DB.Posts_Tbl where Posts_Tbl.delete_flag == false select Posts_Tbl).ToList();
            }
        }
        //Get All Posts End





        //Get Posts Start
        public static PostData_struc Select(long id)
        {
            using (Database_DB DB = new Database_DB())
            {
                PostData_struc post = new PostData_struc();
                var pst= (from Posts_Tbl in DB.Posts_Tbl join Categories_Tbl in DB.Categories_Tbl on Posts_Tbl.category_id.id equals Categories_Tbl.id where Posts_Tbl.id == id && Posts_Tbl.delete_flag == false select new { Posts_Tbl , Categories_Tbl}).First();

                post.Post = pst.Posts_Tbl;
                post.Category = pst.Categories_Tbl;

                return post;
            }
        }
        //Get Posts End




        //Get Delete Start
        public static void Delete(long id)
        {
            using (Database_DB DB = new Database_DB())
            {
                Posts_Tbl post = (from Posts_Tbl in DB.Posts_Tbl where Posts_Tbl.id == id select Posts_Tbl).First();

                post.delete_flag = true;

                DB.SaveChanges();
            }
        }
        //Get Delete End




        //Get Delete Start
        public static void Update(long id,string title, string content, string image, bool visible, long user_id, long cat_id)
        {
            using (Database_DB DB = new Database_DB())
            {

                Users_Tbl usr_id = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id == user_id select Users_Tbl).FirstOrDefault();
                Categories_Tbl categ = (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.id == cat_id select Categories_Tbl).FirstOrDefault();

                Posts_Tbl post = (from Posts_Tbl in DB.Posts_Tbl where Posts_Tbl.id == id select Posts_Tbl).First();

                post.title = title;
                post.content = content;
                post.image = image;
                post.visible = visible;
                post.User_Id = usr_id;
                post.category_id = categ;

                DB.SaveChanges();
            }
        }
        //Get Delete End


    }
}
