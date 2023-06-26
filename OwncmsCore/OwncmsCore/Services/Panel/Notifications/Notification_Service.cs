using OwncmsCore.Models.DatabaseModels;

namespace OwncmsCore.Services.Panel.Notifications
{
    public class Notification_Service
    {

        public struct Notification_Out
        {
            public List<notifications_tbl> notifications;
            public int seen;
        }


        //Get User Notifications Start
        public static Notification_Out Get_All_Notifications(users_tbl user)
        {
            Notification_Out out_put = new Notification_Out();

            using (Database_DB DB = new Database_DB())
            {
                int seen = 0;
                List<notifications_tbl> notifications = (
                    from users_tbl in DB.users_tbl
                    join notifications_tbl in DB.notifications_tbl on users_tbl.id equals notifications_tbl.user_id.id
                    select notifications_tbl).ToList();

                out_put.notifications = notifications;

                for (int i = 0; i < notifications.Count; i++)
                {
                    if (notifications[i].seen == false)
                    {
                        notifications[i].seen = true;
                        seen++;
                    }
                }

                out_put.seen = seen;

                DB.SaveChanges();
            }

            return out_put;
        }
        //Get User Notifications End



        //Get User Notifications Start
        public static List<notifications_tbl> Get_All_Old_Notifications(long user_id)
        {
            using (Database_DB DB = new Database_DB())
            {
                return (from users_tbl in DB.users_tbl
                        join notifications_tbl in DB.notifications_tbl on users_tbl.id equals notifications_tbl.user_id.id
                        where users_tbl.id == user_id
                        select notifications_tbl).ToList();
            }
        }
        //Get User Notifications End



        //Send Notification Start
        public static void Send_Notification(string Sender_Id, string User_Id, string title, string content_text, string link)
        {
            using (Database_DB DB = new Database_DB())
            {
                long User = long.Parse(User_Id);

                users_tbl user = (from Users_Tbl in DB.users_tbl where Users_Tbl.id == User select Users_Tbl).FirstOrDefault();

                notifications_tbl notification = new notifications_tbl();

                notification.title = title;
                notification.content = content_text;
                notification.link = link;
                notification.seen = false;
                notification.from_user = Sender_Id.ToString();
                notification.user_id = user;

                DB.notifications_tbl.Add(notification);
                DB.SaveChanges();
            }
        }
        //Send Notification End

    }
}
