using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using SmpsPortal.Controllers;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        public string UserGroupName { get; set; }

        public IEnumerable<SelectListItem> UserGroupNames { get; set; }

        public IEnumerable<Menu> Menus { get; set; }

        [FutureDate2]
        public DateTime DateExpiry { get; set; }

        public IEnumerable<Announcement> Announcements { get; set; }

        public string Heading { get; set; }

        public bool IsActive { get; set; }

        public bool IsEditable { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<AnnouncementController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<AnnouncementController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;

            }


        }
    }
}