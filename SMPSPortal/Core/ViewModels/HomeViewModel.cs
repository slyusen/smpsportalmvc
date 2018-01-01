using System.Collections.Generic;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Menu> Menus { get; set; }
    }
}