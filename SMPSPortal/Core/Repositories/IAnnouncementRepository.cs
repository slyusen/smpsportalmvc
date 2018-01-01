using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
   public interface IAnnouncementRepository
    {
        IEnumerable<Announcement> GetAnnouncementsUserisInvolvedWith(string userId);
        Announcement GetAnnouncementById(int announcementId);
        IEnumerable<Announcement> GetActiveAnnoucementswithUserGroup();
        IEnumerable<Announcement> GetMyAnnouncements(string userId);
        void Add(Announcement ann);
        void Remove(Announcement ann);
    }
}
