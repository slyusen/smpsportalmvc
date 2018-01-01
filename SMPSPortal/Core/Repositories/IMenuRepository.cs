using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
   public interface IMenuRepository
   {
       IEnumerable<Menu> GetMenusWithMenuItems();
       MenuItem GetlastMenuItemByMenu(Menu menu);
       void AddMenuWithComponents(Menu menu);
       IEnumerable<Menu> GetCapableMenusWithMenuItems(List<string> roleNames);
       IEnumerable<Menu> GetMenus();
       Menu GetLastMenu();
       void AddMenuItem(MenuItem menuItem);
       void AddMenu(Menu menu);
       void AddCapilities(MenuCapability menuCap);
       Menu GetMenuById(int menuId);
   }
}
