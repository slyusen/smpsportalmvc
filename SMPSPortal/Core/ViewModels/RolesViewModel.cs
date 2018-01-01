using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SmpsPortal.Core.ViewModels
{
    public class RolesViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }

        public IEnumerable<Menu> Menus { get; set; }

        public IEnumerable<ApplicationUser> AllUsersInAdmin { get; set; }

        public IEnumerable<ApplicationUser> AllUsersInSchoolAdmin { get; set; }

        public IEnumerable<ApplicationUser> AllUsersInExamOfficer { get; set; }

        public IEnumerable<ApplicationUser> UsersInAdminRole { get; set; }


        public IEnumerable<ApplicationUser> UsersInExamOfficerRole { get; set; }

        public string Heading { get; set; }
    }
}