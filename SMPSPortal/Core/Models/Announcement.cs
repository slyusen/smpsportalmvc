using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SmpsPortal.Core.Models
{
    public class Announcement
    {
        public int Id { get; set; }

        public string AnnouncerId { get; set; }
        public ApplicationUser Announcer { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Given")]
        public DateTime DateGiven { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Expired")]
        public DateTime DateExpired { get; set; }

        public bool IsActive { get; set; }

        public string UserGroupName { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

        public Announcement()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Create()
        {
            var notification = Notification.AnnouncementCreated(this);

            foreach (var attendeee in Attendances.Select(a => a.Attendee))
            {
                attendeee.Notify(notification);
            }
        }

        public bool IsUserInvolved(string userId)
        {
            var attendances = Attendances.Where(b => b.AttendeeId == userId && b.AnnouncementId == Id).ToList();
            if (attendances.Count > 0)
                return true;
            else
                return false;
        }

        public void Modify(DateTime dateExpired, bool isActive, string message, string title, IEnumerable<Attendance> userGroup)
        {

            var notification = Notification.AnnouncementUpdated(this, Title);

            DateExpired = dateExpired;
            IsActive = isActive;
            Message = message;
            Title = title;
            foreach (var attendee in userGroup.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}