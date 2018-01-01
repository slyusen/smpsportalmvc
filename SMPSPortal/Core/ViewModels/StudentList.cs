using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.ViewModels
{
    public class StudentList
    {
        public StudentList(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public StudentList(string id, string studNumber, string imgUrl, string name, string email, string phone, string gLevel, string sClass)
        {
            this.Id = id;
            this.StudentNumber = studNumber;
            this.GradeLevel = gLevel;
            this.Email = email;
            this.ImageUrl = imgUrl;
            this.Name = name;
            this.PhoneNumber = phone;
            this.SchoolClass = sClass;
           
        }

        public string Id { get; set; }

        public string StudentNumber { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string GradeLevel { get; set; }

        public string SchoolClass { get; set; }
        
    }
}