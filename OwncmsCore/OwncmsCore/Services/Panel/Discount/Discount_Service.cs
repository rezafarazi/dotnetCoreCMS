using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Discount
{
    public class Discount_Service
    {

        //Get all discount codes start
        public static List<discount_code_tbl> GetAllDiscountCode()
        {
            using(Database_DB DB=new Database_DB())
            {
                return (from discount_code_tbl in DB.discount_code_tbl where discount_code_tbl.delete_flag=="0" select discount_code_tbl).ToList();
            }
        }
        //Get all discount codes end


        //Gerenrate new discount code start
        public static void Generate(int count)
        {
            using(Database_DB DB=new Database_DB())
            {
                discount_code_tbl code = new discount_code_tbl();

                code.code = CodeGenerator();
                code.delete_flag = "0";
                code.count = count;
                code.date = DateTime.Now.ToString();

                DB.discount_code_tbl.Add(code);
                DB.SaveChanges();
            }
        }
        //Gerenrate new discount code end


        //Code generator start
        internal static string CodeGenerator()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        //Code generator end


        //Get remove discount code start
        public static void GetRmeoveCode(long discount_id)
        {
            using(Database_DB DB=new Database_DB())
            {
                discount_code_tbl code= (from discount_code_tbl in DB.discount_code_tbl where discount_code_tbl.id == discount_id && discount_code_tbl.delete_flag == "0" select discount_code_tbl).First();
                DB.discount_code_tbl.Remove(code);
                DB.SaveChanges();
            }
        }
        //Get remove discount code end


    }
}
