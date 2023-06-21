using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Models.FormModels;

namespace OwncmsCore.Services.Panel.Categories
{
    public class Categories_Service
    {


        //Get select categories start
        public static categories_tbl GetCategory(long id)
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from categories_tbl in DB.categories_tbl where categories_tbl.id == id select categories_tbl).First();
            }
        }
        //Get select categories start


        //Get select all categories start
        public static List<categories_tbl> GetAllCategories()
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from categories_tbl in DB.categories_tbl where categories_tbl.delete_flag == "0" select categories_tbl).ToList();
            }
        }
        //Get select all categories end


        //Insert new category start
        public static void NewCategory(Category_Model category)
        {
            using (Database_DB DB=new Database_DB())
            {
                categories_tbl N_category = new categories_tbl() {
                    title = category.title,
                    root_cat_id = int.Parse(category.root),
                    icon_address = category.iconaddress,
                    visible = category.visible,
                    delete_flag = "0",
                    datetime = DateTime.Now.ToString(),
                    castom_link = category.ca_link,
                };

                DB.categories_tbl.Add(N_category);

                DB.SaveChanges();
            }
        }
        //Insert new category end


        //Update category start
        public static void UpdateCategory(Category_Model category)
        {
            using (Database_DB DB = new Database_DB())
            {
                long cat_id = long.Parse(category.category_id);
                categories_tbl N_category = (from categories_tbl in DB.categories_tbl where categories_tbl.id == cat_id select categories_tbl).First();

                N_category.title = category.title;
                N_category.root_cat_id = int.Parse(category.root);
                N_category.icon_address = category.iconaddress;
                N_category.visible = category.visible;
                N_category.castom_link = category.ca_link;

                DB.SaveChanges();
            }
        }
        //Update category end


        //Delete category start
        public static void DeleteCategory(long category_id)
        {
            using (Database_DB DB = new Database_DB())
            {
                categories_tbl N_category = (from categories_tbl in DB.categories_tbl where categories_tbl.id == category_id select categories_tbl).First();
                N_category.delete_flag = "1";
                DB.SaveChanges();
            }
        }
        //Delete category end


    }
}
