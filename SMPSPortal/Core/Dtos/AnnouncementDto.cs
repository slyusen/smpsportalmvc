using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.Dtos
{
    public class AnnouncementDto
    {
        public int Id { get; set; }

        public UserDto Announcer { get; set; }

        public DateTime DateGiven { get; set; }

        public string Title { get; set; }
    }
}