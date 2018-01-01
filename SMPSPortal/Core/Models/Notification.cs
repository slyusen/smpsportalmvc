using System;
using System.Collections.Generic;
using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Models
{
    public class Notification : Subject
    {
        private List<UserNotification> Observers = new List<UserNotification>();

        public int Id { get; private set; }

        public NotificationType Type { get; private set; }

        public DateTime DateTime { get; private set; }

        public int? AnnouncementId { get; set; }

        public Announcement Announcement { get; private set; }

        public int? AssessmentId { get; set; }

        public Assessment Assessment { get; private set; }

        public string OriginalAnnouncementTitle { get; private set; }


        public void notifyObservers()
        {
            Observers.ForEach(x => x.update(this));
        }
        protected Notification()
        {

        }

        private Notification(Announcement announcement, NotificationType type)
        {
            if (announcement == null)
                throw new ArgumentNullException("announcement");
            Announcement = announcement;
            Type = type;
            DateTime = DateTime.Now;
        }

        private Notification(Assessment assessment, NotificationType type)
        {
            if (assessment == null)
                throw new ArgumentNullException("assessment");
            Assessment = assessment;
            Type = type;
            DateTime = DateTime.Now;
        }

        public static Notification AnnouncementCreated(Announcement newAnnouncement)
        {
            return new Notification(newAnnouncement, NotificationType.AnnouncementCreated);
        }

        public static Notification AssessmentCreated(Assessment ca)
        {
            return new Notification(ca, NotificationType.AssessmentCreated);
        }

        public static Notification AnnouncementUpdated(Announcement editedAnnouncement, string originalAnnouncementTitle)
        {
            var notify = new Notification(editedAnnouncement, NotificationType.AnnouncementUpdated);

            notify.OriginalAnnouncementTitle = originalAnnouncementTitle;

            return notify;
        }
        public void registerObserver(UserNotification observer)
        {
            Observers.Add(observer);
        }

        public void removeObserver(UserNotification observer)
        {
            Observers.Remove(observer);
        }
    }
}