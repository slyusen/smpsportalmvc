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
using SmpsPortal.Persistence;
using SmpsPortal.Persistence.Repository;
using Microsoft.AspNet.Identity;
using Syncfusion.JavaScript;
using Syncfusion.JavaScript.DataSources;
using Syncfusion.Linq;

namespace SmpsPortal.Controllers.Academics
{
    public class GradeLevelController : Controller
    {
        // GET: GradeLevel
        public IUnitofWork _unitofWork;
        private MenuRepository menuRepo;

        public GradeLevelController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            menuRepo =new MenuRepository();
        }

        [Authorize]
        public ActionResult Index()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                   .Where(ck => ck.Type == ClaimTypes.Role)
                   .Select(c => c.Value).ToList(); ;
            var mainmenu = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var gradeLevels = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
            ViewBag.datasource = gradeLevels;
            var gModel = new GradeLevelViewModel()
            {
                Menus = mainmenu,
                GradeLevels = gradeLevels,
                Heading = "Grade Levels"
            };

            return View(gModel);
        }

        public ActionResult GradeLevelDataSource(DataManager dm)
        {
            IEnumerable DataSource = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
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

        public ActionResult GradeLevelUpdate(GradeLevel value)
        {
            _unitofWork._GradeLevelRepository.Update(value);
            _unitofWork.Complete();
            var data = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GradeLevelInsert(GradeLevel value)
        {
           
            GradeLevel grade = new GradeLevel();
            grade.Title = value.Title;
            grade.Code = value.Code;
            _unitofWork._GradeLevelRepository.Add(grade);
            _unitofWork.Complete();
            var data = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

   

        public ActionResult GradeLevelDelete(int key)
        {
            var gl = _unitofWork._GradeLevelRepository.GetGradeLevelById(key);
            _unitofWork._GradeLevelRepository.Remove(gl);
            _unitofWork.Complete();
            var data = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}