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
    public class ProgramsController : Controller
    {
        public IUnitofWork _unitofWork;
        private MenuRepository menuRepo;
        public ProgramsController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            menuRepo = new MenuRepository();
        }
        // GET: Programs
        [Authorize]
        public ActionResult Index()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                    .Where(ck => ck.Type == ClaimTypes.Role)
                    .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var schoolPrograms = _unitofWork._ProgramRepository.GetAllPrograms();
            ViewBag.datasource = schoolPrograms;
            var pModel = new ProgramViewModel()
            {
                Menus = menus,
                Programs = schoolPrograms,
                Heading = "Programs"
            };

            return View(pModel);
        }
        public ActionResult DataSource(DataManager dm)
        {
            IEnumerable DataSource = _unitofWork._ProgramRepository.GetAllPrograms();
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

        public ActionResult ProgramUpdate(Program value)
        {
            _unitofWork._ProgramRepository.Update(value);
            _unitofWork.Complete();
            var data = _unitofWork._ProgramRepository.GetAllPrograms();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProgramInsert(Program value)
        {
            
            Program pro = new Program();
            pro.Name = value.Name;
            pro.Description = value.Description;
            
            _unitofWork._ProgramRepository.Add(pro);
            _unitofWork.Complete();
            var data = _unitofWork._ProgramRepository.GetAllPrograms();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ProgramDelete(int key)
        {
            var pro = _unitofWork._ProgramRepository.GetProgramById(key);
            _unitofWork._ProgramRepository.Remove(pro);
            _unitofWork.Complete();
            var data = _unitofWork._ProgramRepository.GetAllPrograms();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}