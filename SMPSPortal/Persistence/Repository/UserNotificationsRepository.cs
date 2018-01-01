using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class UserNotificationsRepository : IUserNotificationsRepository
    {
        ApplicationDbContext _context;

        public UserNotificationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserNotification> GetAllUserNotifications()
        {
            return _context.UserNotifications.ToList();
        }
        public bool IsUserNotificationExist(string userId)
        {
            return _context.UserNotifications.Any(u => u.NotificationUserId == userId);
        }
        public IEnumerable<UserNotification> GetUserNotifiationsForUser(string userId)
        {
            var userNotifies = _context.UserNotifications.Where(n => n.NotificationUserId == userId && !n.IsRead);
            return userNotifies;
        }
        public Notification GetNotificationById(int id)
        {
            return _context.Notifications.SingleOrDefault(n => n.Id == id);
        }
        public IEnumerable<Notification> GetNotificationsForUser(string userId)
        {


            return _context.UserNotifications
                .Where(n => n.NotificationUserId == userId && !n.IsRead)
                .Select(u => u.Notification)
                .Include(n => n.Announcement.Announcer)
                .Include(g => g.Assessment.Course)
                .ToList();
        }
        public Notification GetNotificationByAssessmentId(int assessmentId)
        {
            return _context.Notifications.SingleOrDefault(n => n.AssessmentId == assessmentId);
        }

        public IEnumerable<UserNotification> GetAllUserNotificationsByNotificationId(int notifyId)
        {
            return _context.UserNotifications.Where(n => n.NotificationId == notifyId);
        }
        public List<UserNotification> GetAllUnreaUserNotifications(string userId)
        {
            return _context.UserNotifications
                 .Where(u => u.NotificationUserId == userId && !u.IsRead).ToList();
        }
        public void Remove(UserNotification userNotification)
        {
            _context.UserNotifications.Remove(userNotification);
        }

        public void RemoveNotification(Notification notify)
        {
            _context.Notifications.Remove(notify);
        }
    }
}