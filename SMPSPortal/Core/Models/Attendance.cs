namespace SmpsPortal.Core.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public ApplicationUser Attendee { get; set; }
        public string AttendeeId { get; set; }

        public int? AssessmentId { get; set; }

        public Assessment Assessment { get; set; }

        public int? AnnouncementId { get; set; }

        public Announcement Announcement { get; set; }
    }
}