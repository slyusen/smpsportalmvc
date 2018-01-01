using SmpsPortal.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmpsPortal.Core.ViewModels
{
    public class MenusViewModel
    {
        public IEnumerable<Menu> Menus { get; set; }
    }
}