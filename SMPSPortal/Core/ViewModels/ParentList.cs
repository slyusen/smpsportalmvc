using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.ViewModels
{
    public class ParentList
    {
        public ParentList()
        {

        }

        public ParentList(string id, string imageUrl, string name, string email, string phone, string occupation)
        {
            this.Id = id;
            this.ImageUrl = imageUrl;
            this.Name = name;
            this.Email = email;
            this.Phone = phone;
            this.Occupation = occupation;
        }
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Occupation { get; set; }
    }
}