using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.Models
{
    public class CaResult
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        public Student Student { get; set; }

        public int AssessmentId { get; set; }

        public Assessment Assessment { get; set; }

        public double Score { get; set; }

        public string Response { get; set; }

        public DateTime? DateSubmitted { get; set; }

        public string Remark { get; set; }
    }
}