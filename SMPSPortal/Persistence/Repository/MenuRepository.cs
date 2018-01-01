using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.Repositories;
using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Persistence.Repository
{
    public class MenuRepository : IMenuRepository
    {
        ApplicationDbContext _context;

        public MenuRepository()
        {
            
                _context = ApplicationDbContext.Create();
            
        }

        public IEnumerable<Menu> GetCapableMenus(List<string> roleNames)
        {
            var caps =  _context.MenuCapabilities.Where(c => roleNames.Contains(c.RoleName)).Select(c => c.MenuId).ToList();
           return _context.Menus.Where(m => caps.Contains(m.Id)).ToList();
            
        }
        public IEnumerable<MenuItem> GetCapableMenuItems(List<string> roleNames)
        {
            var caps = _context.MenuCapabilities.Where(c => roleNames.Contains(c.RoleName)).Select(c => c.MenuItemId).ToList();
            return _context.MenuItems.Where(m => caps.Contains(m.Id)).Include(m => m.Menu).ToList();
        }

       
        public IEnumerable<Menu> GetCapableMenusWithMenuItems(List<string> roleNames)
        {
            var menus = GetCapableMenus(roleNames).Select(m => m.Id);
            var menuItems = GetCapableMenuItems(roleNames).ToArray();
           var menuList =  _context.Menus.Where(m => menus.Contains(m.Id)).OrderBy(m => m.MenuOrder)
                .Include(m => m.MenuItems);

           
            
            foreach (var item in menuList)
            {
               
                item.MenuItems.Clear();
                
                for (int j = 0; j < menuItems.Count(); j++)
                {
                    var menuId = menuItems[j].Menu.Id;
                    
                    
                    if (item.Id == menuId)
                    {
                        item.MenuItems.Add(menuItems[j]);
                    }
                }
               
            }
            
            return menuList;

        }
        public IEnumerable<Menu> GetMenusWithMenuItems()
        {
          return  _context.Menus.OrderBy(m => m.MenuOrder)
                .Include(m => m.MenuItems.OrderBy(n => n.MenuOrder)).ToList();

        }
        public IEnumerable<Menu> GetMenus()
        {
            return _context.Menus.OrderBy(m => m.MenuOrder);

        }

        public MenuItem GetlastMenuItemByMenu(Menu menu)
        {
          return  _context.MenuItems.Last(m => m.Menu.Id == menu.Id);
        }

        public void AddCapilities(MenuCapability menuCap)
        {
            _context.MenuCapabilities.Add(menuCap);
            _context.SaveChanges();
        }

        public void AddMenuWithComponents(Menu menu)
        {
            _context.Menus.Add(menu);
            foreach (var item in menu.menuComponents)
            {
                _context.MenuItems.Add((MenuItem)item);
            }
        }

        public Menu GetMenuById(int menuId)
        {
            return _context.Menus.SingleOrDefault(m => m.Id == menuId);
        }
        public Menu GetLastMenu()
        {
            return _context.Menus.Last();
        }
        public void AddMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
        }
        public void AddMenu(Menu menu)
        {

            _context.Menus.Add(menu);
            _context.SaveChanges();
        }
    }
}