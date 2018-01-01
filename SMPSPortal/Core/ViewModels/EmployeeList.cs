using System;

namespace SmpsPortal.Core.ViewModels
{
    [Serializable]
    public class EmployeeList
    {
        public EmployeeList()
        {

        }

        public EmployeeList(string id, string name, string phone, string imageUrl, string email, string address, string empNumber, DateTime dateEmployed, DateTime dateOfBirth, DateTime dateConfirmed, string departmentTitle, string designationTitle, string type, string category)
        {
            this.Id = id;
            this.Name = name;
            this.PhoneNumber = phone;
            this.ImageUrl = imageUrl;
            this.Email = email;
            this.Address = address;
            this.DateConfirmed = dateConfirmed;
            this.EmployeeNumber = empNumber;
            this.DateEmployed = dateEmployed;
            this.DateofBirth = dateOfBirth;
            this.DepartmentTitle = departmentTitle;
            this.DesignationTitle = designationTitle;
            this.Type = type;
            this.Category = category;
 
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string EmployeeNumber { get; set; }

        public DateTime DateEmployed { get; set; }

        public DateTime DateofBirth { get; set; }

        public DateTime DateConfirmed { get; set; }

        public string DepartmentTitle { get; set; }

        public string DesignationTitle { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }
    }
}