using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using SmpsPortal.Core;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.ViewModels;
using SmpsPortal.Persistence;
using SmpsPortal.Persistence.Repository;
using Microsoft.AspNet.Identity;

namespace SmpsPortal.Controllers.Administration
{
    public class DesignationsController : Controller
    {
        private IUnitofWork _unitofWork;
      
        private IEnumerable<Department> departments;
        private MenuRepository menuRepo;
        private List<DesignationList> dList;

        public DesignationsController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            menuRepo = new MenuRepository();
            dList = new List<DesignationList>();
        }

        public ActionResult Index()
        {

            var roleList = ((ClaimsIdentity)User.Identity).Claims
                   .Where(ck => ck.Type == ClaimTypes.Role)
                   .Select(c => c.Value).ToList(); ;
            var mainmenu = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var pModel = new DesignationViewModel()
            {
                Menus = mainmenu,
                Heading = "Designations"
            };
            var designations = _unitofWork._DesignationRepository.GetAllDesignationsBySchool();
            foreach (var ds in designations)
            {
                var dept = _unitofWork._DepartmentRepository.GetDepartmentById(ds.DepartmentId);
                dList.Add(new DesignationList(ds.Id, ds.Title, dept.Title));
            }
            ViewBag.datasource = dList;

            return View(pModel);
        }
       

        [Authorize]
        public ActionResult Create()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                   .Where(ck => ck.Type == ClaimTypes.Role)
                   .Select(c => c.Value).ToList(); ;
            var mainmenu = menuRepo.GetCapableMenusWithMenuItems(roleList);
            departments = _unitofWork._DepartmentRepository.GetAllDepartments();

            var viewModel = new DesignationViewModel()
            {
                DepartmentList = departments,
                Menus = mainmenu,
                Heading = "Add new Designation"
            };
            return View("DesignationForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                  .Where(ck => ck.Type == ClaimTypes.Role)
                  .Select(c => c.Value).ToList(); ;
            var mainmenu = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var desig = _unitofWork._DesignationRepository.GetDesignationById(id);
            departments = _unitofWork._DepartmentRepository.GetAllDepartments();
            
            var viewModel = new DesignationViewModel()
            {
                DepartmentList = departments,
                DepartmentId = desig.DepartmentId,
                Menus = mainmenu,
                Title = desig.Title,
                Heading = "Edit Designation",
                Id = desig.Id

            };
            return View("DesignationForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DesignationViewModel viewModel)
        {
            departments = _unitofWork._DepartmentRepository.GetAllDepartments();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                  .Where(ck => ck.Type == ClaimTypes.Role)
                  .Select(c => c.Value).ToList(); ;
            var mainmenu = menuRepo.GetCapableMenusWithMenuItems(roleList);

           
            if (!ModelState.IsValid)
            {
                viewModel.DepartmentList = departments;
                viewModel.Menus = mainmenu;
                return View("DesignationForm", viewModel);
            }

            var desig = new Designation()
            {
                Title = viewModel.Title,
                DepartmentId = viewModel.DepartmentId
             
            };

            _unitofWork._DesignationRepository.Add(desig);
            _unitofWork.Complete();
            return RedirectToAction("Index", "Designations");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(DesignationViewModel viewModel)
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                  .Where(ck => ck.Type == ClaimTypes.Role)
                  .Select(c => c.Value).ToList(); ;
            var mainmenu = menuRepo.GetCapableMenusWithMenuItems(roleList);
            departments = _unitofWork._DepartmentRepository.GetAllDepartments();
           
            if (!ModelState.IsValid)
            {
                viewModel.Menus = mainmenu;
                viewModel.DepartmentList = departments;
                return View("DesignationForm", viewModel);
            }


            var desig = _unitofWork._DesignationRepository.GetDesignationById(viewModel.Id);

            desig.Title = viewModel.Title;
            desig.DepartmentId = viewModel.DepartmentId;

            _unitofWork.Complete();
            return RedirectToAction("Index", "Designations");
        }


    }
}