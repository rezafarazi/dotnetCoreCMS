using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Usermanager
{
    public class Usermanager_Service
    {

        public struct GET_ADMIN_USERS_STRUCT
        {
            public List<string> All_User_Id;
            public List<string> All_User_Name;
            public List<string> All_User_Name_And_Family;
        }

        public struct GET_FILL_NEW_USERS_STRUCT
        {
            public List<string> Users_Id;
            public List<string> Users_Days;
            public List<string> Users_Create_Date;
            public List<string> Users_Name_And_Family;
            public List<string> Users_Phone;
            public List<string> Users_Type;
        }


        public struct GET_USER_STRUCT
        {
            public bool Get_User_Edit_Type;
            public String Get_User_Id;
            public String Get_User_Name;
            public String Get_User_Family;
            public String Get_User_Type;
            public String Get_User_Username;
            public String Get_User_Password;
            public String Get_User_City;
            public String Get_User_Email;
            public String Get_User_Phone;
            public String Get_User_Active_Mode;
            public String Get_User_Address;
            public String Get_User_New_Image;
            public String Get_User_User_Type_Name;
            public String Get_User_National_Id;
            public String Get_User_Father_Name;
            public bool Can_Change_Type;
        }
        //Global Variables End


        //Get user start
        public static users_tbl GET_USER(long user_id)
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from users_tbl in DB.users_tbl where users_tbl.id==user_id select users_tbl).First();
            }
        }
        //Get user end


        //Get Admins Data Start
        public static GET_ADMIN_USERS_STRUCT GET_ADMIN_USERS_DATA()
        {
            GET_ADMIN_USERS_STRUCT ADMIN_USERS = new GET_ADMIN_USERS_STRUCT();

            List<string> All_User_Id = new List<string>();
            List<string> All_User_Name = new List<string>();
            List<string> All_User_Name_And_Family = new List<string>();

            using (Database_DB DB = new Database_DB())
            {
                var Get_Admin_Users = from users_tbl in DB.users_tbl
                                      where users_tbl.user_type.ToUpper().Trim() == "ADMIN"
                                      select users_tbl;

                foreach (users_tbl usersTbl in Get_Admin_Users)
                {
                    //Get Fill List Start
                    All_User_Id.Add(usersTbl.id.ToString());
                    All_User_Name.Add(usersTbl.username.ToString());
                    All_User_Name_And_Family.Add(usersTbl.name.ToString() + " " + usersTbl.family.ToString());
                    //Get Fill List End
                }

            }

            ADMIN_USERS.All_User_Id = All_User_Id;
            ADMIN_USERS.All_User_Name = All_User_Name;
            ADMIN_USERS.All_User_Name_And_Family = All_User_Name_And_Family;

            return ADMIN_USERS;
        }
        //Get Admins Data End



        //Get Users Start
        public static GET_FILL_NEW_USERS_STRUCT GET_FILL_NEW_USERS()
        {
            GET_FILL_NEW_USERS_STRUCT NEW_USERS = new GET_FILL_NEW_USERS_STRUCT();

            List<string> Users_Id = new List<string>();
            List<string> Users_Days = new List<string>();
            List<string> Users_Create_Date = new List<string>();
            List<string> Users_Name_And_Family = new List<string>();
            List<string> Users_Phone = new List<string>();
            List<string> Users_Type = new List<string>();

            using (Database_DB DB = new Database_DB())
            {
                var Users = from users_tbl in DB.users_tbl select users_tbl;
                int Count = (Users.Count() < 5) ? Users.Count() : 5;
                for (int i = 0; i < Count; i++)
                {
                    users_tbl User = Users.ToArray()[i];

                    Users_Id.Add(User.id.ToString());
                    Users_Create_Date.Add(User.date_time.ToString());
                    Users_Name_And_Family.Add(User.name.ToString() + " " + User.family.ToString());
                    Users_Phone.Add(User.phone.ToString());


                    if (User.user_type.ToString() == "ADMIN")
                        Users_Type.Add(OwncmsCore.Language.all_strings.ADMIN);
                    else if (User.user_type.ToString() == "ADMIN_USER")
                        Users_Type.Add(OwncmsCore.Language.all_strings.ADMIN_USER);
                    else if (User.user_type.ToString() == "WORKER_USER")
                        Users_Type.Add(OwncmsCore.Language.all_strings.WORKER_USER);
                    else if (User.user_type.ToString() == "USER")
                        Users_Type.Add(OwncmsCore.Language.all_strings.USER);

                }
            }

            NEW_USERS.Users_Id = Users_Id;
            NEW_USERS.Users_Days = Users_Days;
            NEW_USERS.Users_Create_Date = Users_Create_Date;
            NEW_USERS.Users_Name_And_Family = Users_Name_And_Family;
            NEW_USERS.Users_Phone = Users_Phone;
            NEW_USERS.Users_Type = Users_Type;

            return NEW_USERS;
        }
        //Get Users End



        //Get User Search Start
        public static GET_FILL_NEW_USERS_STRUCT GET_USER_SEARCH(string User_Date)
        {

            GET_FILL_NEW_USERS_STRUCT NEW_USERS = new GET_FILL_NEW_USERS_STRUCT();

            List<string> Users_Id = new List<string>();
            List<string> Users_Days = new List<string>();
            List<string> Users_Create_Date = new List<string>();
            List<string> Users_Name_And_Family = new List<string>();
            List<string> Users_Phone = new List<string>();
            List<string> Users_Type = new List<string>();


            using (Database_DB DB = new Database_DB())
            {
                var Search_Result = (from users_tbl in DB.users_tbl
                                     where users_tbl.name.IndexOf(User_Date) > -1
                                     || users_tbl.family.IndexOf(User_Date) > -1
                                     || users_tbl.email.IndexOf(User_Date) > -1
                                     || users_tbl.phone.IndexOf(User_Date) > -1
                                     || users_tbl.date_time.IndexOf(User_Date) > -1
                                     select users_tbl).Distinct();

                Console.WriteLine(Search_Result.Count());

                foreach (users_tbl User in Search_Result)
                {
                    Users_Id.Add(User.id.ToString());
                    Users_Create_Date.Add(User.date_time.ToString());
                    Users_Name_And_Family.Add(User.name.ToString() + " " + User.family.ToString());
                    Users_Phone.Add(User.phone.ToString());


                    if (User.user_type.ToString() == "ADMIN")
                        Users_Type.Add(OwncmsCore.Language.all_strings.ADMIN);
                    else if (User.user_type.ToString() == "ADMIN_USER")
                        Users_Type.Add(OwncmsCore.Language.all_strings.ADMIN_USER);
                    else if (User.user_type.ToString() == "WORKER_USER")
                        Users_Type.Add(OwncmsCore.Language.all_strings.WORKER_USER);
                    else if (User.user_type.ToString() == "USER")
                        Users_Type.Add(OwncmsCore.Language.all_strings.USER);

                }

            }

            NEW_USERS.Users_Id = Users_Id;
            NEW_USERS.Users_Days = Users_Days;
            NEW_USERS.Users_Create_Date = Users_Create_Date;
            NEW_USERS.Users_Name_And_Family = Users_Name_And_Family;
            NEW_USERS.Users_Phone = Users_Phone;
            NEW_USERS.Users_Type = Users_Type;

            return NEW_USERS;
        }
        //Get User Search End



    }
}
