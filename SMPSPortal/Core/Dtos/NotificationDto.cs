using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Dtos
{
    public class NotificationDto
    {
        public NotificationType Type { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime? OriginalDateTime { get; set; }

        public AnnouncementDto Announcement { get; set; }

        public AssessmentDto Assessment { get; set; }

        public string OriginalAnnouncementTitle { get; set; }
    }
}