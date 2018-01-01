using System;
using System.Collections.Generic;
using System.Linq;

using System.Security.Claims;

using System.Web.Mvc;
using SmpsPortal.Core;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.ViewModels;
using SmpsPortal.Persistence;
using SmpsPortal.Persistence.Repository;
using Microsoft.AspNet.Identity;

namespace SmpsPortal.Controllers.Academics
{
    public class SchoolClassesController : Controller
    {
        public IUnitofWork _unitofWork;
        private List<SchoolClassesList> schoolClassesLists;
        private MenuRepository menuRepo;

        public SchoolClassesController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            schoolClassesLists = new List<SchoolClassesList>();
            menuRepo = new MenuRepository();
            ;
        }
        public ActionResult Index()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
               .Where(ck => ck.Type == ClaimTypes.Role)
               .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var pModel = new SchoolClassesViewModel()
            {
                Menus = menus,
                Heading = "Classrooms"
            };
            var scClasses = _unitofWork._SchoolClassRepository.GetAllSchoolClass();
            foreach (var cl in scClasses)
            {
                var prog = _unitofWork._ProgramRepository.GetProgramById(cl.ProgramId);
                var gradeLevel = _unitofWork._GradeLevelRepository.GetGradeLevelById(cl.GradeLevelId);
               

                schoolClassesLists.Add(new SchoolClassesList(cl.Id, cl.Code, cl.Capacity, cl.NumberofElectives, prog.Name, gradeLevel.Title));

            }

            ViewBag.datasource = schoolClassesLists;
            return View(pModel);
        }
       
        [Authorize]
        public ActionResult Create()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var programs = _unitofWork._ProgramRepository.GetAllPrograms();
            var gradeLevels = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
            
            var viewModel = new SchoolClassesViewModel()
            {
                ProgramList = programs,
                GradeLevelList = gradeLevels,
                Menus = menus,
                Heading = "Add new Class"
            };
            return View("SchoolClassForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            var scClass = _unitofWork._SchoolClassRepository.GetSchoolClassById(id);
            var programs = _unitofWork._ProgramRepository.GetAllPrograms();
            var gradeLevels = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var viewModel = new SchoolClassesViewModel()
            {
                Id = scClass.Id,
                Code = scClass.Code,
                NumberofElectives = scClass.NumberofElectives,
                Capacity = scClass.Capacity,
                Description = scClass.Description,
                ProgramId = scClass.ProgramId,
                GradeLevelId = scClass.GradeLevelId,
                ProgramList = programs,
                GradeLevelList = gradeLevels,
               
                Menus = menus,
                Heading = "Edit Class"
            };
            return View("SchoolClassForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SchoolClassesViewModel viewModel)
        {
            var programs = _unitofWork._ProgramRepository.GetAllPrograms();
            var gradeLevels = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
             .Where(ck => ck.Type == ClaimTypes.Role)
             .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
           
            if (!ModelState.IsValid)
            {
                viewModel.ProgramList = programs;
             
                viewModel.GradeLevelList = gradeLevels;
                viewModel.Menus = menus;
                return View("SchoolClassForm", viewModel);
            }

            var scClass = new SchoolClass()
            {
                Code = viewModel.Code,
                Description = viewModel.Description,
                Capacity = viewModel.Capacity,
                NumberofElectives = viewModel.NumberofElectives,
                GradeLevelId = viewModel.GradeLevelId,
                ProgramId = viewModel.ProgramId
               
            };

            _unitofWork._SchoolClassRepository.Add(scClass);
            _unitofWork.Complete();
            return RedirectToAction("Index", "SchoolClasses");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SchoolClassesViewModel viewModel)
        {
            var programs = _unitofWork._ProgramRepository.GetAllPrograms();
            var gradeLevels = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();

            var roleList = ((ClaimsIdentity)User.Identity).Claims
             .Where(ck => ck.Type == ClaimTypes.Role)
             .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            if (!ModelState.IsValid)
            {
                viewModel.ProgramList = programs;
                viewModel.GradeLevelList = gradeLevels;
                viewModel.Menus = menus;
                return View("SchoolClassForm", viewModel);
            }


            var scClass = _unitofWork._SchoolClassRepository.GetSchoolClassById(viewModel.Id);

            scClass.Modify(viewModel.Code, viewModel.Capacity, viewModel.NumberofElectives, viewModel.Description, viewModel.ProgramId, viewModel.GradeLevelId);

            _unitofWork.Complete();
            return RedirectToAction("Index", "SchoolClasses");
        }
    }
}
