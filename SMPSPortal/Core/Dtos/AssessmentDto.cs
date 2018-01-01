using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.Dtos
{
    public class AssessmentDto
    {
        public int Id { get; set; }

        public CourseDto Course { get; set; }

        public DateTime DateDue { get; set; }

        public string Type { get; set; }
    }
}