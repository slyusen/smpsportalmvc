using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.ViewModels
{
    public class StudentParentList
    {
        public StudentParentList()
        {

        }

        public StudentParentList(int id, string studentName, string parentName, string relationship)
        {
            this.Id = id;
            this.StudentName = studentName;
            this.ParentName = parentName;
            this.Relationship = relationship;
        }

        public string StudentName { get; set; }
        public string ParentName { get; set; }
        public string Relationship { get; set; }
        public int Id { get; set; }
    }
}