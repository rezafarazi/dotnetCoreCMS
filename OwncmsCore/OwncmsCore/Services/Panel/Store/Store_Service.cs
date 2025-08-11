using OwncmsCore.Models.DatabaseModels;
using OwncmsCore.Models.FormModels;
using OwncmsCore.Services.File;
using OwncmsCore.Services.Panel.Categories;

namespace OwncmsCore.Services.Panel.Store
{
    public class Store_Service
    {

        //Add new store start
        public static async Task AddNewStoreItem(users_tbl user,NewStore_Model model)
        {
            using(Database_DB DB=new Database_DB())
            {

                store_categories_tbl category = Store_Categories_Service.GetCategory(long.Parse(model.category));

                file_tbl file = null;

                if(model.file.Length > 0)
                {
                    file = await File_Service.NewFile(model.file);
                }

                store_tbl store = new store_tbl() {

                    title = model.title,
                    content = model.cntent,
                    price = model.price,
                    type = model.type,
                    user_id = user,
                    visible = model.visible,
                    json = model.attr,
                    date = DateTime.Now.ToString(),
                    discount = "0",
                    delete_flag = "0",
                    adiscount = model.price,
                    category_id = category,
                    file_id=file

                };

                DB.store_tbl.Add(store);


                string []images = model.all_images.Split(',');
                foreach (string image in images)
                {
                    file_tbl img = File_Service.GetFile(image);
                    store_images_tbls str_img = new store_images_tbls() { date = DateTime.Now.ToString(), file_id = img, store_id = store };
                    DB.store_images_tbls.Add(str_img);
                }

                DB.SaveChanges();

            }
        }
        //Add new store end

    }
}
