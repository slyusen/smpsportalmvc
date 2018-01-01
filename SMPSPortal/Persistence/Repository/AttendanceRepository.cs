using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        IApplicationDbContext _context;
        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public bool IsUserAnnouncement(string userId, int announcementId)
        {
            return _context.Attendances.Any(a => a.AnnouncementId == announcementId && a.AttendeeId == userId);
        }
        public bool IsUserAssessment(string userId, int assessmentId)
        {
            return _context.Attendances.Any(a => a.AssessmentId == assessmentId && a.AttendeeId == userId);
        }
        public IEnumerable<Attendance> GetAllAttendance()
        {
            return _context.Attendances.ToList();
        }

        public void Add(Attendance att)
        {
            _context.Attendances.Add(att);
        }

        public void Remove(Attendance att)
        {
            _context.Attendances.Remove(att);
        }
    }
}