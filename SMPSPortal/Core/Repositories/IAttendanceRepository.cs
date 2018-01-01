using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
    public interface IAttendanceRepository
    {
        bool IsUserAnnouncement(string userId, int announcementId);
        bool IsUserAssessment(string userId, int assessmentId);
        IEnumerable<Attendance> GetAllAttendance();
        void Add(Attendance att);
        void Remove(Attendance att);
    }
}
