using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using SmpsPortal.Controllers;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class MenuViewModel
    {
        
        [Required]
        [StringLength(100, ErrorMessage = "Menu Name too Long")]
        [Display(Name = "Menu Title")]
        public string Name { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "Menu Url too Long")]
        [Display(Name = "Url")]
        public string Url { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "Description too Long")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Parent Menu")]
        public int? ParentMenuId { get; set; }

        public IEnumerable<Menu> ParentMenu { get; set; }
        
        [Display(Name = "Role Capabilities")]
        public string[] RoleId { get; set; }

        public MultiSelectList RoleList { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<HomeController, ActionResult>> menu = (c => c.Menu(this));
               
                var action = menu;
                return (action.Body as MethodCallExpression).Method.Name;

            }


        }

    }
}