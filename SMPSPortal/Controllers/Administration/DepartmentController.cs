using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using SmpsPortal.Core;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Syncfusion.JavaScript;
using Syncfusion.JavaScript.DataSources;
using Syncfusion.Linq;
using SmpsPortal.Persistence;
using SmpsPortal.Persistence.Repository;

namespace SmpsPortal.Controllers.Administration
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public IUnitofWork _unitofWork;
        private MenuRepository menuRepo;
        public DepartmentController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            menuRepo = new MenuRepository();
        }
        public ActionResult Index()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                    .Where(ck => ck.Type == ClaimTypes.Role)
                    .Select(c => c.Value).ToList(); ;
            var mainmenu = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var departments = _unitofWork._DepartmentRepository.GetAllDepartments();

            var pModel = new DepartmentViewModel()
            {
                Menus = mainmenu,
                Departments = departments,
                Heading = "Departments"
            };

            return View(pModel);
        }

        public ActionResult DepartmentDataSource(DataManager dm)
        {
            IEnumerable DataSource = _unitofWork._DepartmentRepository.GetAllDepartments();
            DataResult result = new DataResult();
            DataOperations operation = new DataOperations();
            result.result = DataSource;
            result.count = result.result.AsQueryable().Count();
            if (dm.Skip > 0)
                result.result = operation.PerformSkip(result.result, dm.Skip);
            if (dm.Take > 0)
                result.result = operation.PerformTake(result.result, dm.Take);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult DepartmentUpdate(Department value)
        {
            var depts = ViewData["Departments"];


            _unitofWork._DepartmentRepository.Update(value);
            _unitofWork.Complete();
            var data = _unitofWork._DepartmentRepository.GetAllDepartments();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DepartmentInsert(Department value)
        {
            Department dept = new Department();
            dept.Title = value.Title;
            dept.Abbreviation = value.Abbreviation;
            _unitofWork._DepartmentRepository.Add(dept);
            _unitofWork.Complete();
            var data = _unitofWork._DepartmentRepository.GetAllDepartments();
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public ActionResult DepartmentDelete(int key)
        {
            var dept = _unitofWork._DepartmentRepository.GetDepartmentById(key);
            _unitofWork._DepartmentRepository.Remove(dept);
            _unitofWork.Complete();
            var data = _unitofWork._DepartmentRepository.GetAllDepartments();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}