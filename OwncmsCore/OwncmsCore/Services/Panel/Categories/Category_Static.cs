using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Categories
{ 
    public class Category_Class
    {
        
        //Insert Start
        public struct Generate_Categorys_Struct
        {
            public List<string> Categorys;
            public List<string> Categorys_id;
        }
        //Insert End

        //Table Start
        public struct Generate_Categprys_Tables_Strcut
        {
            public List<string> Categorys_Table_Id;
            public List<string> Categorys_Table_Name;
            public List<string> Categorys_Table_Logo_Address;
            public List<string> Categorys_Table_Visible;
            public List<string> Categorys_Table_Root;
        }
        //Table End

        //Edit Profile Start
        public struct Fill_Feilds_Records_Strcut
        {
            public string Edit_Catgory_Id;
            public string Edit_Catgory_Name;
            public string Edit_Catgory_Logo;
            public string Edit_Catgory_Root;
            public string Edit_Catgory_Visisble;
            public string Edit_Catgory_City_Id;
            public string Edit_Catgory_Header;
            public string Edit_Catgory_Text;
            public bool Edit_Catgory_TwoWay;
        }
        //Edit Profile End
            


        //Get Categorys For Insert Start
        public static Generate_Categorys_Struct Get_Generate_Categorys_Name()
        {
            Generate_Categorys_Struct Generate_Categorys = new Generate_Categorys_Struct();

            List<string> Categorys=new List<string>();
            List<string> Categorys_id=new List<string>();

            using (Database_DB DB = new Database_DB())
            {
                var Get_All_Categorys = from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.Enabled_Flag!="DELETED" select Categories_Tbl;

                Categorys.Clear();

                foreach(Categories_Tbl Item in Get_All_Categorys)
                {
                    Categorys_id.Add(Item.id.ToString());
                    Categorys.Add(Item.title);
                }

            }

            Generate_Categorys.Categorys = Categorys;
            Generate_Categorys.Categorys_id = Categorys_id;

            return Generate_Categorys;

        }
        //Get Categorys For Insert End


        //Get Categorys For Table Start
        public static Generate_Categprys_Tables_Strcut Get_Generate_Categprys_Tables_Data()
        {

            Generate_Categprys_Tables_Strcut Categprys_Tables = new Generate_Categprys_Tables_Strcut();

            List<string> Categorys_Table_Id=new List<string>();
            List<string> Categorys_Table_Name=new List<string>();
            List<string> Categorys_Table_Logo_Address=new List<string>();
            List<string> Categorys_Table_Visible=new List<string>();
            List<string> Categorys_Table_Root=new List<string>();
            List<string> Categorys_Table_City = new List<string>();

            using (Database_DB DB = new Database_DB())
            {
                var Get_All_Categorys = (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.Enabled_Flag != "DELETED" select new { Categories_Tbl}).ToList();

                Categorys_Table_Id.Clear();
                Categorys_Table_Name.Clear();
                Categorys_Table_Logo_Address.Clear();
                Categorys_Table_Root.Clear();
                Categorys_Table_Visible.Clear();

                for (int i = 0; i < Get_All_Categorys.Count; i++)
                {
                    Categorys_Table_Id.Add(Get_All_Categorys[i].Categories_Tbl.id.ToString());
                    Categorys_Table_Name.Add(Get_All_Categorys[i].Categories_Tbl.title);
                    Categorys_Table_Logo_Address.Add(Get_All_Categorys[i].Categories_Tbl.Icon_Address);
                    Categorys_Table_Root.Add(Get_All_Categorys[i].Categories_Tbl.Root_Id);
                    Categorys_Table_Visible.Add(Get_All_Categorys[i].Categories_Tbl.Enabled_Flag);
                }

            }

            Categprys_Tables.Categorys_Table_Id = Categorys_Table_Id;
            Categprys_Tables.Categorys_Table_Name = Categorys_Table_Name;
            Categprys_Tables.Categorys_Table_Logo_Address = Categorys_Table_Logo_Address;
            Categprys_Tables.Categorys_Table_Visible = Categorys_Table_Visible;
            Categprys_Tables.Categorys_Table_Root = Categorys_Table_Root;

            return Categprys_Tables;
        }
        //Get Categorys For Table End
        

        //Update Category Start
        public static void UPDATE_Category(int Cat_Id,string Title,string Logo,string Root_Cat,string Visible,long city_id,string header,string text, string two_way)
        {
            using (Database_DB DB = new Database_DB())
            {
                var Category = from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.id == Cat_Id select Categories_Tbl;

                foreach (Categories_Tbl Row in Category)
                {
                    Row.title = Title;
                    Row.Icon_Address = Logo;
                    Row.Root_Id = Root_Cat;
                    Row.Enabled_Flag = Visible;
                    Row.city_id = city_id;
                    Row.header_image = header;
                    Row.text = text;

                    if (two_way != null)
                    {
                        if (two_way.ToString().Trim().ToUpper() == "ON")
                        {
                            Row.two_way = true;
                        }
                        else
                        {
                            Row.two_way = false;
                        }
                    }
                }

                DB.SaveChanges();
            }
        }
        //Update Category End


        //Get Fill Fields Start
        public static Fill_Feilds_Records_Strcut Fill_Feilds_Records(int id)
        {
            Fill_Feilds_Records_Strcut Fill_Feilds_Records = new Fill_Feilds_Records_Strcut();
            
            using (Database_DB DB = new Database_DB())
            {
                var Category = (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.id==id select Categories_Tbl).FirstOrDefault();
                
                Fill_Feilds_Records.Edit_Catgory_Id = Category.id.ToString();
                Fill_Feilds_Records.Edit_Catgory_Name = Category.title.ToString();
                Fill_Feilds_Records.Edit_Catgory_Logo = Category.Icon_Address.ToString();
                Fill_Feilds_Records.Edit_Catgory_City_Id = Category.city_id.ToString();
                Fill_Feilds_Records.Edit_Catgory_Header = Category.header_image.ToString();
                Fill_Feilds_Records.Edit_Catgory_Text = Category.text.ToString();
                Fill_Feilds_Records.Edit_Catgory_TwoWay = Category.two_way;
                if (Category.Root_Id.ToString().ToUpper() == "ROOT")
                {
                    Fill_Feilds_Records.Edit_Catgory_Root = "";
                }
                else
                {
                    Fill_Feilds_Records.Edit_Catgory_Root = Category.Root_Id.ToString();
                }
                Fill_Feilds_Records.Edit_Catgory_Visisble = Category.Enabled_Flag.ToString();
            }

            return Fill_Feilds_Records;
        }
        //Get Fill Fields End
        
        
        //New Category Start
        public static void INSERT_NEW_CATEGORY(long User_Id,string Visibleity,string Category_Name,string Icon_Address,string Root_Cat,long city_id,string header_image,string text, string two_way)
        {
            using (Database_DB DB = new Database_DB())
            {

                var user = from Users_Tbl in DB.Users_Tbl where Users_Tbl.id == User_Id select Users_Tbl;
                
                Categories_Tbl Category = new Categories_Tbl();
                Category.User_Id = user.ToArray()[0];
                Category.Create_Date = PersianDateTime.Now + "";
                Category.Enabled_Flag = Visibleity;
                Category.title = Category_Name;
                Category.Icon_Address = Icon_Address;
                Category.city_id = city_id;
                Category.header_image = header_image;
                Category.text = text;
                if (Root_Cat == "Root")
                {
                    Category.Type = "Root";
                    Category.Root_Id = "";
                }
                else
                {
                    Category.Type = "Sub";
                    Category.Root_Id = Root_Cat;
                }

                if (two_way != null)
                {
                    if (two_way.ToString().Trim().ToUpper() == "ON")
                    {
                        Category.two_way = true;
                    }
                    else
                    {
                        Category.two_way = false;
                    }
                }

                DB.Categories_Tbl.Add(Category);
                

                DB.SaveChanges();
                
            }
        }
        //New Category End


        //Delete Category Start
        public static void Delete_CATEGORY(string Catrgory_Id)
        {
            using (Database_DB DB = new Database_DB())
            {
                long category_id = long.Parse(Catrgory_Id);
                Categories_Tbl cat = (from Categories_Tbl in DB.Categories_Tbl where Categories_Tbl.id == category_id select Categories_Tbl).FirstOrDefault();
                cat.Enabled_Flag = "DELETED";
                DB.SaveChanges();
            }
        }
        //Delete Category End


    }
}