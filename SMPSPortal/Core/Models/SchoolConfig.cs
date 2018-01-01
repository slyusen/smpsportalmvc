using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.Models
{
    public class SchoolConfig
    {
        public int Id { get; set; }
       

        public string WebAdress { get; set; }

        public double CAPercent { get; set; }

        public double ExamPercent { get; set; }

        public YearTerm CurrentYearTerm { get; set; }

        public int CurrentYearTermId { get; set; }
    }
}