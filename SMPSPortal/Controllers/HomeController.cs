using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SmpsPortal.Core;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.ViewModels;
using SmpsPortal.Persistence;
using SmpsPortal.Persistence.Repository;
using Microsoft.AspNet.Identity;

namespace SmpsPortal.Controllers
{
    public class HomeController : Controller
    {
        public IUnitofWork _unitofWork;
        private MenuRepository menuRepo;

        public HomeController()
        {
          _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            menuRepo = new MenuRepository();
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new HomeViewModel();
                
                var roleList = ((ClaimsIdentity)User.Identity).Claims
                    .Where(ck => ck.Type == ClaimTypes.Role)
                    .Select(c => c.Value).ToList();
                
               
                var menus =  menuRepo.GetCapableMenusWithMenuItems(roleList);
                
                model.Menus = menus;
                return View(model);
            }
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }
        public PartialViewResult LoginAction()
        {
            if (User.Identity.IsAuthenticated)
            {
                var cUser = _unitofWork._UserRepository.GetSingleUser(User.Identity.GetUserId());

                var vmodel = new IndexViewModel()
                {
                    ProfileImageUrl = cUser.ProfileImageUrl,
                    CurrentUserName = cUser.FullName
                };
                return PartialView("~/Views/Shared/_LoginPartial.cshtml", vmodel);
            }
            else
            {
                var vmodel = new IndexViewModel()
                {
                    ProfileImageUrl = ""
                };
                return PartialView("~/Views/Shared/_LoginPartial.cshtml", vmodel);
            }
        }

        public PartialViewResult MenuAction()
        {
            var model = new MenusViewModel();
            if (User.Identity.IsAuthenticated)
            {
                

                var roleList = ((ClaimsIdentity)User.Identity).Claims
                    .Where(ck => ck.Type == ClaimTypes.Role)
                    .Select(c => c.Value).ToList();


                var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

                model.Menus = menus;
                return PartialView("~/Views/Shared/_Menus.cshtml", model);
            }

            else
            {
                return PartialView("~/Views/Shared/_Menus.cshtml", model);
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Menu()
        {
            var model = new MenuViewModel();

            var parentMenus = menuRepo.GetMenus();
            var roleMgr = _unitofWork._UserRepository.GetRoleManager();
            model.ParentMenu = parentMenus;
            
            model.RoleList = new MultiSelectList(roleMgr.Roles.ToList(), "Name", "Name");
            return View(model);
        }

        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public ActionResult Menu(MenuViewModel model)
        {
           
           
                var roles = model.RoleId.ToList();
                if (model.ParentMenuId == null)
                {
                    var menu = new Menu();
                    menu.Name = model.Name;
                    model.Description = model.Description;
                    model.ParentMenuId = 0;
                    menu.Url = model.Url;
                    menuRepo.AddMenu(menu);
                    _unitofWork.Complete();

                    foreach (var role in roles)
                    {
                        var menuCap = new MenuCapability();
                        menuCap.MenuId = menu.Id;
                        menuCap.RoleName = role;
                        menuCap.AllowedAction = ActionType.Edit;
                        menuRepo.AddCapilities(menuCap);
                    }
                    _unitofWork.Complete();

                }
                else
                {
                    var menu = menuRepo.GetMenuById((int)model.ParentMenuId);
                    var menuItem = new MenuItem(model.Name, model.Url, 0, model.Description);
                    menuRepo.AddMenuItem(menuItem);
                    _unitofWork.Complete();
                    
                    foreach (var role in roles)
                    {
                        var menuCap = new MenuCapability();
                        menuCap.MenuItemId = menuItem.Id;
                        menuCap.RoleName = role;
                        menuCap.AllowedAction = ActionType.Edit;
                        menuRepo.AddCapilities(menuCap);
                    }
                    _unitofWork.Complete();
                   
                }




            return RedirectToAction("Menu", "Home");
        }

        public
            ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}