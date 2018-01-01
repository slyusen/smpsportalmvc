using System;

namespace SmpsPortal.Core.Models
{
    public class UserNotification : Observer
    {
        public int Id { get; set; }

        public string NotificationUserId { get; set; }

        public ApplicationUser NotificationUser { get; set; }

        public int NotificationId { get; set; }

        public Notification Notification { get; set; }

        public bool IsRead { get; private set; }

        protected UserNotification()
        {

        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (notification == null)
                throw new ArgumentNullException("notify");

            NotificationUser = user;
            Notification = notification;
        }

        public void MarkIsRead()
        {
            IsRead = true;
        }


        public void update(Notification notification)
        {
            Notification = notification;
        }
    }
}