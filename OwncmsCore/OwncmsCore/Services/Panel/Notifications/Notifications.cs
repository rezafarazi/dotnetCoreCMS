using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OwncmsCore.Models.DatabaseModels;

namespace Own_Cms.Functions.Statics.Notifications
{
    public class Notifications
    {


        public struct Notification_Out{
            public List<Notifications_Tbl> notifications;
            public int seen;
        }


        //Get User Notifications Start
        public static Notification_Out Get_All_Notifications(Users_Tbl user)
        {
            Notification_Out out_put =new Notification_Out();

            using (Database_DB DB = new Database_DB())
            {
                int seen = 0;
                List<Notifications_Tbl> notifications = (
                    from Users_Tbl in DB.Users_Tbl
                    join Notifications_Tbl in DB.Notifications_Tbl on Users_Tbl.id equals Notifications_Tbl.user_id.id
                    select Notifications_Tbl).ToList();

                out_put.notifications = notifications;
                for (int i = 0; i < notifications.Count; i++)
                {
                    if (notifications[i].seen == "0")
                    {
                        notifications[i].seen = "1";
                        seen++;
                    }
                }
                out_put.seen = seen;

                Users_Tbl usr = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id == user.id select Users_Tbl).FirstOrDefault();
                usr.notification = false;

                DB.SaveChanges();
            }

            return out_put;
        }
        //Get User Notifications End



        //Get User Notifications Start
        public static List<Notifications_Tbl> Get_All_Old_Notifications(long user_id)
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from Users_Tbl in DB.Users_Tbl
                        join Notifications_Tbl in DB.Notifications_Tbl on Users_Tbl.id equals Notifications_Tbl.user_id.id
                        where Users_Tbl.id==user_id
                        select Notifications_Tbl).ToList();
            }
        }
        //Get User Notifications End



        //Send Notification Start
        public static void Send_Notification(string Sender_Id,string User_Id, string title, string content_text, string link)
        {
            using (Database_DB DB = new Database_DB())
            {
                long User = long.Parse(User_Id);

                Users_Tbl user = (from Users_Tbl in DB.Users_Tbl where Users_Tbl.id == User select Users_Tbl).FirstOrDefault();
                user.notification = true;

                Notifications_Tbl notification = new Notifications_Tbl();

                notification.title = title;
                notification.content = content_text;
                notification.link = link;
                notification.seen = "0";
                notification.from = Sender_Id.ToString();
                notification.user_id = user;

                DB.Notifications_Tbl.Add(notification);
                DB.SaveChanges();
            }
        }
        //Send Notification End


    }
}