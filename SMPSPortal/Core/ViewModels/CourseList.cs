using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.ViewModels
{
    public class CourseList
    {
        public CourseList()
        {

        }
        public CourseList(int id, string code, string title, string programTitle, string gradeLevelCode, string classCode)
        {
            this.Id = id;
            this.Code = code;
            this.Title = title;
            this.ProgramTitle = programTitle;
            this.GradeLevelCode = gradeLevelCode;
            this.ClassCode = classCode;
        }
        public int Id { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public string ProgramTitle { get; set; }

        public string GradeLevelCode { get; set; }

        public string ClassCode { get; set; }
    }
}