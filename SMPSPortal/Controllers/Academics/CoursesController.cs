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
    public class CoursesController : Controller
    {
        // GET: Courses
        private IUnitofWork _unitofWork;
        private SchoolConfig school;
        private List<CourseList> dList, rgList;
        private MenuRepository menuRepo;

        public CoursesController()
        {
            menuRepo = new MenuRepository();
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            dList = new List<CourseList>();
            rgList = new List<CourseList>();

        }

        public ActionResult Index()
        {

            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var pModel = new CourseViewModel()
            {
                Menus = menus,
                Heading = "Courses"
            };
            var courses = _unitofWork._CourseRepository.GetAllCourses();
            foreach (var crs in courses)
            {
                var prog = _unitofWork._ProgramRepository.GetProgramById(crs.ProgramId);
                var gLevel = _unitofWork._GradeLevelRepository.GetGradeLevelById((int)crs.GradeLevelId);
                var cClass = _unitofWork._SchoolClassRepository.GetSchoolClassById((int)crs.SchoolClassId);
                dList.Add(new CourseList(crs.Id, crs.Code, crs.Title, prog.Name, gLevel.Code, cClass.Code));
            }
            ViewBag.datasource = dList;

            return View(pModel);
        }
       

        [Authorize]
        public ActionResult MyCourses()
        {
            string userRole = "";
            var teacherRole = User.IsInRole("Teacher");
            var studentRole = User.IsInRole("Student");
           
            var courses = new List<Course>();
            var registeredCourses = new List<Course>();
            if (teacherRole)
            {
                userRole = "Teacher";
                
                var teacherCourses = _unitofWork._TeacherCourseRepository.GetAllAssignedCoursesForTeacher(User.Identity.GetUserId());
                courses = teacherCourses.ToList();
            }
            if (studentRole)
            {
                userRole = "Student";
               var student = _unitofWork._StudentRepository.GetStudentById(User.Identity.GetUserId());
                var studentcourses = _unitofWork._CourseRepository.GetAllCoursesByClassCategory(student.SchoolClassId, CourseCategory.Compulsory);
                courses = studentcourses.ToList();
                registeredCourses =
                    _unitofWork._CourseRegisterRepository.GetCoursesByStudentClass(student.Id, student.SchoolClassId)
                        .ToList();
            }
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var pModel = new CourseViewModel()
            {
                Menus = menus,
                Heading = "My Courses",
                UserType = userRole
            };


            foreach (var crs in courses)
            {
                var prog = _unitofWork._ProgramRepository.GetProgramById(crs.ProgramId);
                var gLevel = _unitofWork._GradeLevelRepository.GetGradeLevelById((int)crs.GradeLevelId);
                var cClass = _unitofWork._SchoolClassRepository.GetSchoolClassById((int)crs.SchoolClassId);
                dList.Add(new CourseList(crs.Id, crs.Code, crs.Title, prog.Name, gLevel.Code, cClass.Code));
            }



            ViewBag.datasource = dList;


            return View(pModel);
        }
        public ActionResult MyCourseDataSource(DataManager dm)
        {
          
            var student = _unitofWork._StudentRepository.GetStudentById(User.Identity.GetUserId());

            var registeredCourses =
                _unitofWork._CourseRegisterRepository.GetCoursesByStudentClass(student.Id, student.SchoolClassId)
                    .ToList();

            foreach (var crs in registeredCourses)
            {
                var prog = _unitofWork._ProgramRepository.GetProgramById(crs.ProgramId);
                var gLevel = _unitofWork._GradeLevelRepository.GetGradeLevelById((int)crs.GradeLevelId);
                var cClass = _unitofWork._SchoolClassRepository.GetSchoolClassById((int)crs.SchoolClassId);
                rgList.Add(new CourseList(crs.Id, crs.Code, crs.Title, prog.Name, gLevel.Code, cClass.Code));
            }
            IEnumerable DataSource = rgList;
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

        public ActionResult MyCourseDelete(int key)
        {
          
            var student = _unitofWork._StudentRepository.GetStudentById(User.Identity.GetUserId());
            var regCourse = _unitofWork._CourseRegisterRepository.GetCourseByStudentClass(key, student.Id,
                student.SchoolClassId);


            _unitofWork._CourseRegisterRepository.Remove(regCourse);
            _unitofWork.Complete();
            var registeredCourses =
                _unitofWork._CourseRegisterRepository.GetCoursesByStudentClass(student.Id, student.SchoolClassId)
                    .ToList();

            foreach (var crs in registeredCourses)
            {
                var prog = _unitofWork._ProgramRepository.GetProgramById(crs.ProgramId);
                var gLevel = _unitofWork._GradeLevelRepository.GetGradeLevelById((int)crs.GradeLevelId);
                var cClass = _unitofWork._SchoolClassRepository.GetSchoolClassById((int)crs.SchoolClassId);
                rgList.Add(new CourseList(crs.Id, crs.Code, crs.Title, prog.Name, gLevel.Code, cClass.Code));
            }
            var data = rgList;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult RegisterElectives()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
             .Where(ck => ck.Type == ClaimTypes.Role)
             .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var student = _unitofWork._StudentRepository.GetStudentById(User.Identity.GetUserId());
            var courses = _unitofWork._CourseRepository.GetAllCoursesByClassCategory
                (student.SchoolClassId, CourseCategory.Elective);


            var pModel = new CourseViewModel()
            {
                Menus = menus,
                Heading = "Register Electives",
                RegCourseList = courses

            };
            return View(pModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult RegisterElectives(CourseViewModel viewModel)
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
               .Where(ck => ck.Type == ClaimTypes.Role)
               .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var student = _unitofWork._StudentRepository.GetStudentById(User.Identity.GetUserId());
            var courses = _unitofWork._CourseRepository.GetAllCoursesByClassCategory
                (student.SchoolClassId, CourseCategory.Elective);

            var registeredCourses = _unitofWork._CourseRegisterRepository.GetCourseRegistersByStudentClass(student.Id,
                student.SchoolClassId);
            var sclass = _unitofWork._SchoolClassRepository.GetSchoolClassById(student.SchoolClassId);
            if (registeredCourses.Count() >= sclass.NumberofElectives)
            {
                var pModel = new CourseViewModel()
                {
                    Menus = menus,
                    Heading = "Register Course",
                    RegCourseList = courses,
                    Message = "Maximum Allowed Electives Reached"

                };
                return View(pModel);
            }
            var regCourseChk = _unitofWork._CourseRegisterRepository.CheckCourseByStudentClass(viewModel.RegCourseId,
                student.Id, student.SchoolClassId);
            if (regCourseChk)
            {
                var pModel = new CourseViewModel()
                {
                    Menus = menus,
                    Heading = "Register Course",
                    RegCourseList = courses,
                    Message = "You have Registered this course for this class already"

                };
                return View(pModel);
            }
            
         
            var newCourseReg = new CourseRegister()
            {
                CourseId = viewModel.RegCourseId,
                SchoolClassId = student.SchoolClassId,
                StudentId = student.Id,
                YearTermId = school.CurrentYearTermId

            };

            _unitofWork._CourseRegisterRepository.Add(newCourseReg);
            _unitofWork.Complete();

            return RedirectToAction("MyCourses", "Courses");
        }

        [Authorize]
        public ActionResult Create()
        {
          
            var progs = _unitofWork._ProgramRepository.GetAllPrograms();
            var gLevels = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
            var cClasses = _unitofWork._SchoolClassRepository.GetAllSchoolClass();

            var roleList = ((ClaimsIdentity)User.Identity).Claims
            .Where(ck => ck.Type == ClaimTypes.Role)
            .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var viewModel = new CourseViewModel()
            {
                ProgramList = progs,
                GradeLevelList = gLevels,
                SchoolClassList = cClasses,
                Menus = menus,
                Heading = "Add new Course"
            };
            return View("CourseForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            var crs = _unitofWork._CourseRepository.GetCourseById(id);
            var progs = _unitofWork._ProgramRepository.GetAllPrograms();
            var gLevels = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
            var cClasses = _unitofWork._SchoolClassRepository.GetAllSchoolClass();

            var roleList = ((ClaimsIdentity)User.Identity).Claims
             .Where(ck => ck.Type == ClaimTypes.Role)
             .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var viewModel = new CourseViewModel()
            {
                ProgramList = progs,
                GradeLevelList = gLevels,
                SchoolClassList = cClasses,
                Menus = menus,
                Title = crs.Title,
                Code = crs.Code,
                Category = crs.Category,
                ProgramId = crs.ProgramId,
                GradeLevelId = (int)crs.GradeLevelId,
                SchoolClasId = (int)crs.SchoolClassId,
                Heading = "Edit Course",
                Id = crs.Id

            };
            return View("CourseForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            var progs = _unitofWork._ProgramRepository.GetAllPrograms();
            var gLevels = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
            var cClasses = _unitofWork._SchoolClassRepository.GetAllSchoolClass();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            if (!ModelState.IsValid)
            {
                viewModel.ProgramList = progs;
                viewModel.GradeLevelList = gLevels;
                viewModel.SchoolClassList = cClasses;
                viewModel.Menus = menus;
                return View("CourseForm", viewModel);
            }

            var crs = new Course()
            {
                Title = viewModel.Title,
                ProgramId = viewModel.ProgramId,
                Code = viewModel.Code,
                Category = viewModel.Category,
                GradeLevelId = viewModel.GradeLevelId,
                SchoolClassId = viewModel.SchoolClasId
            };
         

            _unitofWork._CourseRepository.Add(crs);
            _unitofWork.Complete();
            return RedirectToAction("Index", "Courses");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModel viewModel)
        {
            var progs = _unitofWork._ProgramRepository.GetAllPrograms();
            var gLevels = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
            var cClasses = _unitofWork._SchoolClassRepository.GetAllSchoolClass();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
             .Where(ck => ck.Type == ClaimTypes.Role)
             .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            if (!ModelState.IsValid)
            {
                viewModel.ProgramList = progs;
                viewModel.GradeLevelList = gLevels;
                viewModel.SchoolClassList = cClasses;
                viewModel.Menus = menus;
                return View("CourseForm", viewModel);
            }


            var crs = _unitofWork._CourseRepository.GetCourseById(viewModel.Id);

            crs.Modify(viewModel.Title, viewModel.Code, viewModel.Category, viewModel.ProgramId, viewModel.GradeLevelId, viewModel.SchoolClasId);

            _unitofWork.Complete();
            return RedirectToAction("Index", "Courses");
        }
    }
}