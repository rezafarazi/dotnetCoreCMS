using OwncmsCore.Models.DatabaseModels;
using PersianTools.Core;

namespace OwncmsCore.Services.Panel.Post
{
    public class Post_Service
    {

        public struct PostData_struc
        {
            public posts_tbls Post;
            public categories_tbl Category;
        }



        //Add New Post Start
        public static void Insert(string title, string content, string image, bool visible, long user_id, long cat_id)
        {
            using (Database_DB DB = new Database_DB())
            {

                users_tbl usr_id = (from users_tbl in DB.users_tbl where users_tbl.id == user_id select users_tbl).FirstOrDefault();
                categories_tbl categ = (from categories_tbl in DB.categories_tbl where categories_tbl.id == cat_id select categories_tbl).FirstOrDefault();

                posts_tbls post = new posts_tbls();

                post.title = title;
                post.content = content;
                post.visible = (visible)?"1":"0";
                post.delete_flag = "0";
                post.date = PersianDateTime.Now.ToString();
                post.user_id = usr_id;
                post.category_id = categ;

                if (!string.IsNullOrEmpty(image))
                    post.image = image;

                DB.posts_tbls.Add(post);
                DB.SaveChanges();
            }
        }
        //Add New Post End



        //Get All Posts Start
        public static List<posts_tbls> Select()
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from posts_tbls in DB.posts_tbls where posts_tbls.delete_flag == "0" select posts_tbls).ToList();
            }
        }
        //Get All Posts End





        //Get Posts Start
        public static PostData_struc Select(long id)
        {
            using (Database_DB DB = new Database_DB())
            {
                PostData_struc post = new PostData_struc();
                var pst = (from posts_tbls in DB.posts_tbls join categories_tbl in DB.categories_tbl on posts_tbls.category_id.id equals categories_tbl.id where posts_tbls.id == id && posts_tbls.delete_flag == "0" select new { posts_tbls, categories_tbl }).First();

                post.Post = pst.posts_tbls;
                post.Category = pst.categories_tbl;

                return post;
            }
        }
        //Get Posts End




        //Get Delete Start
        public static void Delete(long id)
        {
            using (Database_DB DB = new Database_DB())
            {
                posts_tbls post = (from posts_tbls in DB.posts_tbls where posts_tbls.id == id select posts_tbls).First();

                post.delete_flag = "0";

                DB.SaveChanges();
            }
        }
        //Get Delete End




        //Get Delete Start
        public static void Update(long id, string title, string content, string image, bool visible, long user_id, long cat_id)
        {
            using (Database_DB DB = new Database_DB())
            {

                users_tbl usr_id = (from users_tbl in DB.users_tbl where users_tbl.id == user_id select users_tbl).FirstOrDefault();
                categories_tbl categ = (from categories_tbl in DB.categories_tbl where categories_tbl.id == cat_id select categories_tbl).FirstOrDefault();

                posts_tbls post = (from posts_tbls in DB.posts_tbls where posts_tbls.id == id select posts_tbls).First();

                post.title = title;
                post.content = content;
                post.image = image;
                post.visible = (visible)?"1":"0";
                post.user_id = usr_id;
                post.category_id = categ;

                DB.SaveChanges();
            }
        }
        //Get Delete End



    }
}
