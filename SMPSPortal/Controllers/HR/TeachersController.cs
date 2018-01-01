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

namespace SmpsPortal.Controllers.HR
{
    public class TeachersController : Controller
    {
        private IUnitofWork _unitofWork;
        private string userId;
        private MenuRepository menuRepo;


        private SchoolConfig school;
        private List<EmployeeList> empList;


        public TeachersController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            empList = new List<EmployeeList>();
            menuRepo = new MenuRepository();

        }

        public ActionResult Index()
        {
            userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity) User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var pModel = new EmployeeViewModel()
            {
                Menus = menus,
                Heading = "Teachers"
            };
            var teachers = _unitofWork._EmployeeRepository.GetAllTeachers();
            foreach (var emp in teachers)
            {
                var dept = _unitofWork._DepartmentRepository.GetDepartmentById((int) emp.DepartmentId);
                var designation = _unitofWork._DesignationRepository.GetDesignationById((int) emp.DesignationId);


                var category = Enum.GetName(typeof(EmployeeCategory), emp.EmployeeCategory);
                var type = Enum.GetName(typeof(EmployeeType), emp.EmployeeType);

                empList.Add(new EmployeeList(emp.Id, emp.FullName, emp.PhoneNumber, emp.ProfileImageUrl, emp.Email,
                    emp.Address, emp.EmployeeNumber, emp.DateEmployed, emp.DateofBirth, emp.DateConfirmed, dept.Title,
                    designation.Title, type, category));
              
            }
            ViewBag.datasource = empList;

            return View(pModel);
        }

        public ActionResult AssignCourses ()
        {
                userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
             .Where(ck => ck.Type == ClaimTypes.Role)
             .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var teachers = _unitofWork._EmployeeRepository.GetAllTeachers();
                var allCourses = _unitofWork._CourseRepository.GetAllCourses();
                var assignedCourses = _unitofWork._TeacherCourseRepository.GetAllTeacherCourses();
                var teachCourses = new List<TeacherCourseList>();
                var allTeachers = new List<TeacherList>();

                foreach (var asc in assignedCourses)
                {
                    var crs = _unitofWork._CourseRepository.GetCourseById(asc.CourseId);
                    var emp = _unitofWork._EmployeeRepository.GetEmployeeById(asc.EmployeeId);
                   
                    teachCourses.Add(new TeacherCourseList(crs.Id, emp.Id, crs.Code, emp.FullName));
                }
                foreach (var te in teachers)
                {
                    
                    allTeachers.Add(new TeacherList(te.Id, te.FullName));
                }
                var tModel = new TeacherCourseViewModel()
                {
                    Heading = "Assign Courses to Teacher",
                    Teachers = allTeachers,
                    AllCourses = allCourses,
                    AssignedCourses = teachCourses,
                    Menus = menus
                };
                return View(tModel);
        }
    }
}
