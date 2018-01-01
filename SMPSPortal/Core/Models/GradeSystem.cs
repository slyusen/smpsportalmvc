using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.Models
{
    public class GradeSystem
    {
        public int Id { get; set; }

        public double Points { get; set; }

        public string Grade { get; set; }

        public string Definition { get; set; }

        public double UpBoundary { get; set; }

        public double DownBoundary { get; set; }

        public int Equivalent { get; set; }
        
    }
}