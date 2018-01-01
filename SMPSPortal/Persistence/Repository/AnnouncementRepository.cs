using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        IApplicationDbContext _context;
        private string url;

        public AnnouncementRepository(ApplicationDbContext context)
        {
            _context = context;
            url = "http://localhost";
        }

       
        public IEnumerable<Announcement> GetAnnouncementsUserisInvolvedWith(string userId)
        {
            var myUserNotificationsId = _context.UserNotifications.Where(u => u.NotificationUserId == userId).Select(u => u.NotificationId);

            var myAnnouncementNotifications =
                _context.Notifications.Where(n => n.AnnouncementId != null && myUserNotificationsId.Contains(n.Id));
            var myAnnouncementIds = myAnnouncementNotifications.Select(a => a.AnnouncementId);
            return _context.Announcements
                .Include(g => g.Announcer)
                .ToList()
                .Where(g => g.IsActive && g.DateExpired > DateTime.Now && myAnnouncementIds.Contains(g.Id));
        }

        public Announcement GetAnnouncementById(int announcementId)
        {
            return _context.Announcements.SingleOrDefault(a => a.Id == announcementId);
        }

        public IEnumerable<Announcement> GetActiveAnnoucementswithUserGroup()
        {
            var school = _context.SchoolConfigs.SingleOrDefault(s => s.WebAdress == url);
            return _context.Announcements
                .Where(g =>  g.IsActive)
                .Include(g => g.Attendances).ToList();
        }
        public IEnumerable<Announcement> GetMyAnnouncements(string userId)
        {
            return _context.Announcements
                .Where(g => g.AnnouncerId == userId)
                .ToList();
        }
        public void Add(Announcement ann)
        {
            _context.Announcements.Add(ann);
        }

        public void Remove(Announcement ann)
        {
            _context.Announcements.Remove(ann);
        }
    }
}