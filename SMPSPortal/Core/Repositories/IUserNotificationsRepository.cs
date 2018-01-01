using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
   public interface IUserNotificationsRepository
    {
        IEnumerable<UserNotification> GetAllUserNotifications();
        IEnumerable<Notification> GetNotificationsForUser(string userId);
        List<UserNotification> GetAllUnreaUserNotifications(string userId);
        Notification GetNotificationByAssessmentId(int assessmentId);
        IEnumerable<UserNotification> GetAllUserNotificationsByNotificationId(int notifyId);
        IEnumerable<UserNotification> GetUserNotifiationsForUser(string userId);
        Notification GetNotificationById(int id);
        bool IsUserNotificationExist(string userId);
        void Remove(UserNotification userNotification);
        void RemoveNotification(Notification notify);
    }
}
