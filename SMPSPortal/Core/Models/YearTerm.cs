using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.Models
{
    public class YearTerm
    {
        public int Id { get; set; }

        public DateTime Year { get; set; }

        public string Term { get; set; }

        public string Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}