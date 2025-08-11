using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Settings
{
    public class Setting_Service
    {

        //Get setting start
        public static settings_tbl GetSettings()
        {
            using(Database_DB DB=new Database_DB())
            {
                return (from settings_tbl in DB.settings_tbl orderby settings_tbl.id descending select settings_tbl).First();
            }
        }
        //Get setting end


        //Remove discount start
        public static void SetDiscount(string value)
        {
            using (Database_DB DB = new Database_DB())
            {

                settings_tbl set = (from settings_tbl in DB.settings_tbl orderby settings_tbl.id descending select settings_tbl).First();

                set.discountall = value;

                DB.SaveChanges();
            }
        }
        //Remove discount end


        //New setting start
        public static void AddNewSetting(string parametrs,string discount)
        {
            using(Database_DB DB=new Database_DB())
            {

                discount = "0";
                if(discount=="")
                {
                    var dis = (from settings_tbl in DB.settings_tbl orderby settings_tbl.id descending select settings_tbl).First();
                    discount = dis.discountall;
                }

                settings_tbl set = new settings_tbl() {
                    parametrs = parametrs,
                    discountall = discount
                };

                DB.settings_tbl.Add(set);
                DB.SaveChanges();
            }
        }
        //New setting end

    }
}
