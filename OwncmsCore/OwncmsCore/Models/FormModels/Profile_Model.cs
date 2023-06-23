namespace OwncmsCore.Models.FormModels
{
    public class Profile_Model
    {

        public long id { get; set; } = 0;
        public string username { get; set; }
        public string password { get; set; } = "";
        public string user_type { get; set; } = "";
        public string name { get; set; } = "";
        public string family { get; set; } = "";
        public string email { get; set; } = "";
        public string phone { get; set; } = "";
        public string bio { get; set; } = "";
        public string address { get; set; } = "";
        public string city { get; set; }
        public IFormFile profile_image { get; set; }


    }
}
