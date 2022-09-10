using System;
using System.Collections.Generic;
using System.Linq;
using Hasher_Lib;
using OwncmsCore.Models.DatabaseModels;
using System.Web;
using System.IO;
using OwncmsCore.Models.Data_Models.Update_Profile_Model;

namespace OwncmsCore.Services.Panel.UserManager
{
    public class UserManager
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
        
        
        //Get Admins Data Start
        public static GET_ADMIN_USERS_STRUCT GET_ADMIN_USERS_DATA()
        {
            GET_ADMIN_USERS_STRUCT ADMIN_USERS = new GET_ADMIN_USERS_STRUCT();

            List<string> All_User_Id=new List<string>();
            List<string> All_User_Name=new List<string>();
            List<string> All_User_Name_And_Family=new List<string>();
            
            using (Database_DB DB=new Database_DB())
            {
                var Get_Admin_Users = from Users_Tbl in DB.Users_Tbl
                    where Users_Tbl.user_type.ToUpper().Trim() == "ADMIN" 
                    select Users_Tbl;
                
                foreach (Users_Tbl usersTbl in Get_Admin_Users)
                {   
                    //Get Fill List Start
                    All_User_Id.Add(usersTbl.id.ToString());
                    All_User_Name.Add(usersTbl.username.ToString());
                    All_User_Name_And_Family.Add(usersTbl.name.ToString()+" "+usersTbl.family.ToString());
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

            List<string> Users_Id=new List<string>();
            List<string> Users_Days=new List<string>();
            List<string> Users_Create_Date=new List<string>();
            List<string> Users_Name_And_Family=new List<string>();
            List<string> Users_Phone=new List<string>();
            List<string> Users_Type=new List<string>();
            
            using (Database_DB DB = new Database_DB())
            {
                var Users =from Users_Tbl in DB.Users_Tbl select Users_Tbl;
                int Count = (Users.Count() < 5) ? Users.Count() : 5;
                for (int i = 0; i < Count; i++)
                {
                    Users_Tbl User = Users.ToArray()[i];
                    
                    Users_Id.Add(User.id.ToString());
                    Users_Days.Add(User.peyment_day_count.ToString());
                    Users_Create_Date.Add(User.create_datetime.ToString());
                    Users_Name_And_Family.Add(User.name.ToString()+" "+User.family.ToString());
                    Users_Phone.Add(User.phone.ToString());
                    
                    
                    if(User.user_type.ToString()=="ADMIN")
                        Users_Type.Add(OwncmsCore.Language.all_strings.ADMIN);
                    else if(User.user_type.ToString()== "ADMIN_USER")
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
                var Search_Result = (from Users_Tbl in DB.Users_Tbl
                    where Users_Tbl.name.IndexOf(User_Date) > -1 
                    ||    Users_Tbl.family.IndexOf(User_Date) > -1
                    ||    Users_Tbl.email.IndexOf(User_Date) > -1
                    ||    Users_Tbl.phone.IndexOf(User_Date) > -1
                    ||    Users_Tbl.create_datetime.IndexOf(User_Date) > -1
                    ||    Users_Tbl.city.IndexOf(User_Date) > -1
                    select Users_Tbl).Distinct();

                Console.WriteLine(Search_Result.Count());

                foreach (Users_Tbl User in Search_Result)
                {
                    Users_Id.Add(User.id.ToString());
                    Users_Days.Add(User.peyment_day_count.ToString());
                    Users_Create_Date.Add(User.create_datetime.ToString());
                    Users_Name_And_Family.Add(User.name.ToString()+" "+User.family.ToString());
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
        
        
        
        //Get User Data Fetch Start
        public static GET_USER_STRUCT GET_USER(long User_Id,long Admin_Id)
        {
            GET_USER_STRUCT GET_USER = new GET_USER_STRUCT();

            if (GET_CHECK_Edit_USER(Admin_Id) || User_Id == Admin_Id)
            {
                GET_USER.Get_User_Edit_Type = true;
            }
            else
            {
                GET_USER.Get_User_Edit_Type = false;
            }

            if (User_Id == Admin_Id)
            {
                GET_USER.Can_Change_Type = false;
            }
            else
            {
                GET_USER.Can_Change_Type = true;
            }
            
            using (Database_DB DB = new Database_DB())
            {
                var User = from Users_Tbl in DB.Users_Tbl where Users_Tbl.id == User_Id select Users_Tbl;

                GET_USER.Get_User_Id = User_Id.ToString();
                GET_USER.Get_User_Name =User.ToList()[0].name;
                GET_USER.Get_User_Family =User.ToList()[0].family;
                GET_USER.Get_User_Username =User.ToList()[0].username;
                GET_USER.Get_User_Password =User.ToList()[0].password;
                GET_USER.Get_User_City =User.ToList()[0].city;
                GET_USER.Get_User_Email =User.ToList()[0].email;
                GET_USER.Get_User_Phone =User.ToList()[0].phone;
                GET_USER.Get_User_Active_Mode =User.ToList()[0].active_user_flag;
                GET_USER.Get_User_Address =User.ToList()[0].address;
                GET_USER.Get_User_New_Image = User.ToList()[0].profile_image_address;
                GET_USER.Get_User_Type = User.ToList()[0].user_type.ToUpper();
                GET_USER.Get_User_National_Id = User.ToList()[0].national_id;
                GET_USER.Get_User_Father_Name = User.ToList()[0].father_name;


                if (User.ToList()[0].user_type.ToUpper().Trim() == "ADMIN")
                {
                    GET_USER.Get_User_User_Type_Name = "مدیر اصلی";
                }
                else if (User.ToList()[0].user_type.ToUpper().Trim() == "USER")
                {
                    GET_USER.Get_User_User_Type_Name = "کاربر";
                }
                
            }

            return GET_USER;

        }
        //Get User Data Fetch End
        
        
        
        
        //Get Check Admin User Start
        public static bool GET_CHECK_Edit_USER(long User_Id)
        {
            using (Database_DB DB = new Database_DB())
            {
                var Get_User = from Users_Tbl in DB.Users_Tbl where User_Id == Users_Tbl.id select Users_Tbl;
                
                if (Get_User.ToArray()[0].user_type.ToString().ToString().ToUpper().Trim() == "ADMIN")
                {
                    return true;
                }
            }
            return false;
        }
        //Get Check Admin User End
        
        
        //Get Update User Data Start
        public static void UPDATE_USER_DATA(long Updater_User_Id,Update_Profile_Model user,string userprofileimageaddress)
        {
            long u_id = long.Parse(user.user_id);
            if (GET_CHECK_Edit_USER(Updater_User_Id))
            {
                using (Database_DB DB = new Database_DB())
                {
                    long update_user_id = long.Parse(user.user_id.ToString().Trim());
                    Users_Tbl usr = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id == update_user_id select Users_Tbl).FirstOrDefault();

                    usr.name = user.name;
                    usr.family = user.family;
                    usr.username = user.username;
                    usr.phone = user.phone;
                    usr.email = user.email;
                    usr.active_user_flag = user.status;
                    usr.father_name = user.father_name;
                    usr.national_id = user.national_id;
                    usr.city = user.city;
                    usr.address = user.address;
                    usr.user_type = user.user_type;
                    usr.profile_image_address = userprofileimageaddress;


                    try
                    {
                        if (user.password.ToString().Trim() != string.Empty)
                        {
                            usr.password = Hasher_Lib.Hash.SHA_256(user.password);
                        }
                    }
                    catch
                    {

                    }

                    DB.SaveChanges();
                }
            }
        }
        //Get Update User Data End

            
        //Get Update Profile Start
        public static void Get_Update_Profile(Update_Profile_Model user,string profileimage)
        {
            using (Database_DB DB=new Database_DB())
            {
                long u_id = long.Parse(user.user_id.Trim());
                Users_Tbl user_data = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id == u_id select Users_Tbl).FirstOrDefault();

                user_data.name = user.name;
                user_data.family = user.family;
                user_data.username = user.username;
                user_data.phone = user.phone;
                user_data.email = user.email;
                user_data.national_id = user.national_id;
                user_data.father_name = user.father_name;
                user_data.city = user.city;
                user_data.address = user.address;

                if (user.password != null)
                {
                    user_data.password = Hasher_Lib.Hash.SHA_256(user.password);
                }

                if(profileimage != string.Empty)
                {
                    user_data.profile_image_address = profileimage;
                }

                DB.SaveChanges();
            }
        }
        //Get Update Profile End



        //Add 30 Days Payment Start
        public static void GetAdd30DayPeyment(long User_Id,string value,long pay_user_id,byte day_count)
        {
            using(Database_DB DB=new Database_DB())
            {
                Users_Tbl user = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id == User_Id select Users_Tbl).FirstOrDefault();

                int Last_Days = user.peyment_day_count + day_count;
                user.peyment_day_count = Last_Days;

                DB.SaveChanges();
            }
        }
        //Add 30 Days Payment End



    }
}