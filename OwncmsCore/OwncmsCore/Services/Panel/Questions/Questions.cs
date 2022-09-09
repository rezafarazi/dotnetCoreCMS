using OwncmsCore.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Own_Cms.Functions.Statics.Questions
{
    public class Questions
    {
        
        //Globla Variabls Start
        public struct Categories_Var{
            public List<Categories_Tbl> category;
            public List<Cites_Tbl> city;
        }
        //Globla Variabls End


        //Get Select All Questions Start
        public static Categories_Var Get_All_Question_Categories()
        {

            Categories_Var Cats = new Categories_Var();

            using (Database_DB DB = new Database_DB())
            {
                var Categories = (from Categories_Tbl in DB.Categories_Tbl
                                 join Cites_Tbl in DB.Cites_Tbl 
                                 on Categories_Tbl.city_id equals Cites_Tbl.id
                                 select new { categores=Categories_Tbl , city=Cites_Tbl }).ToList();

                List<Categories_Tbl> category = new List<Categories_Tbl>();
                List<Cites_Tbl> city = new List<Cites_Tbl>();

                for (int i = 0; i < Categories.Count; i++)
                {
                    category.Add(Categories[i].categores);
                    city.Add(Categories[i].city);
                }

                Cats.category = category;
                Cats.city = city;

                return Cats;
            }
        }
        //Get Select All Questions End


        //Get Select All Questions Start
        public static string Get_Questions_Of_Cat(long Category_Id)
        {    
            using (Database_DB DB = new Database_DB())
            {
                var Categories = (from Categories_Tbl in DB.Categories_Tbl
                                  join Cites_Tbl in DB.Cites_Tbl
                                  on Categories_Tbl.city_id equals Cites_Tbl.id
                                  join Question_Tbl in DB.Question_Tbl
                                  on Categories_Tbl.id equals Question_Tbl.Cat_Id.id
                                  where Categories_Tbl.id == Category_Id
                                  select new { categores = Categories_Tbl, city = Cites_Tbl , questions = Question_Tbl}).FirstOrDefault();
                

                return Categories.questions.Json_Questions;
            }
        }
        //Get Select All Questions End


        //Get Updata Question Category Start
        public static void Get_Question_Update(long Category_Id,string json)
        {
            using (Database_DB DB = new Database_DB())
            {
                var Question = (from Categories_Tbl in DB.Categories_Tbl
                                join Question_Tbl in DB.Question_Tbl on Categories_Tbl.id equals Question_Tbl.Cat_Id.id
                                where Question_Tbl.Cat_Id.id == Category_Id select Question_Tbl ).FirstOrDefault();

                Question.Json_Questions = json;
                DB.SaveChanges();
            }
        }
        //Get Updata Question Category End

    }
}