using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class CaResultViewModel
    {
        public int CaId { get; set; }

        public string Title { get; set; }

        public double PercentofCa { get; set; }

        public DateTime DateGiven { get; set; }

        public DateTime DateDue { get; set; }

        public string CourseName { get; set; }

        public string ClassName { get; set; }

        public double HighestScore { get; set; }

        public IEnumerable<Menu> Menus { get; set; }

        public string Heading { get; set; }
    }
}