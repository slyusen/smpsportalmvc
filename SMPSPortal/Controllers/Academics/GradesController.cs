using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmpsPortal.Core;
using SmpsPortal.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Syncfusion.JavaScript;
using Syncfusion.JavaScript.DataSources;
using Syncfusion.Linq;
using System.Security.Claims;
using SmpsPortal.Persistence;
using SmpsPortal.Persistence.Repository;

namespace SmpsPortal.Controllers.Academics
{
    public class GradesController : Controller
    {
        private IUnitofWork _unitofWork;
        private List<AcademicResultList> myResults;
        private List<StudentList> studList;
        private MenuRepository menuRepo;

        public GradesController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
          
            myResults = new List<AcademicResultList>();
            studList = new List<StudentList>();
            menuRepo= new MenuRepository();
            

        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var cclass = _unitofWork._SchoolClassRepository.GetAllSchoolClass();

            var userRole = User.IsInRole("ExamOfficer");

            var firstClass = cclass.First();

            var students = _unitofWork._StudentRepository.GetAllActiveStudentsByClass(firstClass.Id);

            foreach (var student in students)
            {
                var stUser = _unitofWork._UserRepository.GetSingleUser(student.Id);
                studList.Add(new StudentList(student.Id, stUser.FullName));
            }
            var firstStudent = students.First();
            var cPosition = _unitofWork._AcademicRecordRepository.GetByStudentClassYearTerm(firstStudent.Id, firstStudent.SchoolClassId).FirstOrDefault();
            if (!userRole)
                RedirectToAction("Index", "Home");
            int pos = 0;
            if (cPosition != null)
            {
                pos = cPosition.Position;
            }


            var pModel = new GradesViewModel()
            {
                Menus = menus,
                Heading = "View Grades",
                ClassList = cclass,
                StudentList = studList,
                Position = pos
            };
            ViewBag.datasource = null;
            return View(pModel);
        }

        public ActionResult ViewGrades()
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
               .Where(ck => ck.Type == ClaimTypes.Role)
               .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var userRole = User.IsInRole("Parent");
            var parent = _unitofWork._ParentRepository.GetParentById(userId);

            var students = _unitofWork._StudentParentRepository.GetAllStudentParentsByParent(parent.Id).Select(p => p.Student);

            foreach (var student in students)
            {
               
                studList.Add(new StudentList(student.Id, student.FullName));
            }
            var firstStudent = students.First();
            var cPosition = _unitofWork._AcademicRecordRepository.GetByStudentClassYearTerm(firstStudent.Id, firstStudent.SchoolClassId).FirstOrDefault();
            if (!userRole)
                RedirectToAction("Index", "Home");

            int pos = 0;
            if (cPosition != null)
            {
                pos = cPosition.Position;
            }

            var pModel = new GradesViewModel()
            {
                Menus = menus,
                Heading = "View Grades",
                StudentList = studList,
                Position = pos
            };
            ViewBag.datasource = null;
            return View(pModel);
        }

        public ActionResult MyGrades()
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
             .Where(ck => ck.Type == ClaimTypes.Role)
             .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var userRole = User.IsInRole("Student");
          
            var firstStudent = _unitofWork._StudentRepository.GetStudentById(userId);
            var cPosition = _unitofWork._AcademicRecordRepository.GetByStudentClassYearTerm(firstStudent.Id, firstStudent.SchoolClassId).FirstOrDefault();
            if (!userRole)
                RedirectToAction("Index", "Grades");

            int pos = 0;
            if (cPosition != null)
            {
                pos = cPosition.Position;
            }

            var pModel = new GradesViewModel()
            {
                Menus = menus,
                Heading = "My Grades",
                Position = pos
            };
            ViewBag.datasource = null;
            return View(pModel);
        }

        public ActionResult GetDataSource(DataManager dm)
        {
            var userId = User.Identity.GetUserId();
            var scclass = _unitofWork._SchoolClassRepository.GetAllSchoolClass().First();
            var student = _unitofWork._StudentRepository.GetAllActiveStudentsByClass(scclass.Id).First();
            var DataSource = _unitofWork._GradeRepository.GetAllGradesByStudentClassYearTerm(student.Id, scclass.Id).ToList();
            var studUser = _unitofWork._UserRepository.GetSingleUser(student.Id);
            foreach (var da in DataSource)
            {
                var clsName = _unitofWork._SchoolClassRepository.GetSchoolClassById(da.SchoolClassId);

                var course = _unitofWork._CourseRepository.GetCourseById(da.CourseId);
                var gSys = _unitofWork._GradeRepository.GetGradeSystemById(da.GradeSystemId);

                myResults.Add(new AcademicResultList(da.Id, studUser.FullName, da.ExamScore, da.TotalCAScore, course.Code, scclass.Code, gSys.Grade));
            }
            DataResult result = new DataResult();
            DataOperations operation = new DataOperations();
            result.result = myResults;
            result.count = result.result.AsQueryable().Count();
            if (dm.Skip > 0)
                result.result = operation.PerformSkip(result.result, dm.Skip);
            if (dm.Take > 0)
                result.result = operation.PerformTake(result.result, dm.Take);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataSource2(DataManager dm)
        {
            var userId = User.Identity.GetUserId();
            var parent = _unitofWork._ParentRepository.GetParentById(userId);

            var students = _unitofWork._StudentParentRepository.GetAllStudentParentsByParent(parent.Id).Select(p => p.Student);
            var student = students.FirstOrDefault();
            var DataSource = _unitofWork._GradeRepository.GetAllGradesByStudentClassYearTerm(student.Id, student.SchoolClassId).ToList();
            var studUser = _unitofWork._UserRepository.GetSingleUser(student.Id);
            var scclass = _unitofWork._SchoolClassRepository.GetSchoolClassById(student.SchoolClassId);
            foreach (var da in DataSource)
            {
                var clsName = _unitofWork._SchoolClassRepository.GetSchoolClassById(da.SchoolClassId);

                var course = _unitofWork._CourseRepository.GetCourseById(da.CourseId);
                var gSys = _unitofWork._GradeRepository.GetGradeSystemById(da.GradeSystemId);

                myResults.Add(new AcademicResultList(da.Id, studUser.FullName, da.ExamScore, da.TotalCAScore, course.Code, scclass.Code, gSys.Grade));
            }
            DataResult result = new DataResult();
            DataOperations operation = new DataOperations();
            result.result = myResults;
            result.count = result.result.AsQueryable().Count();
            if (dm.Skip > 0)
                result.result = operation.PerformSkip(result.result, dm.Skip);
            if (dm.Take > 0)
                result.result = operation.PerformTake(result.result, dm.Take);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDataSource3(DataManager dm)
        {
            var userId = User.Identity.GetUserId();

            var student = _unitofWork._StudentRepository.GetStudentById(userId);
            var DataSource = _unitofWork._GradeRepository.GetAllGradesByStudentClassYearTerm(student.Id, student.SchoolClassId).ToList();
            var studUser = _unitofWork._UserRepository.GetSingleUser(student.Id);
            var scclass = _unitofWork._SchoolClassRepository.GetSchoolClassById(student.SchoolClassId);
            foreach (var da in DataSource)
            {
                var clsName = _unitofWork._SchoolClassRepository.GetSchoolClassById(da.SchoolClassId);

                var course = _unitofWork._CourseRepository.GetCourseById(da.CourseId);
                var gSys = _unitofWork._GradeRepository.GetGradeSystemById(da.GradeSystemId);

                myResults.Add(new AcademicResultList(da.Id, studUser.FullName, da.ExamScore, da.TotalCAScore, course.Code, scclass.Code, gSys.Grade));
            }
            DataResult result = new DataResult();
            DataOperations operation = new DataOperations();
            result.result = myResults;
            result.count = result.result.AsQueryable().Count();
            if (dm.Skip > 0)
                result.result = operation.PerformSkip(result.result, dm.Skip);
            if (dm.Take > 0)
                result.result = operation.PerformTake(result.result, dm.Take);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadResult(string studentId)
        {
            var student = _unitofWork._StudentRepository.GetStudentById(studentId);
            var scclass = _unitofWork._SchoolClassRepository.GetSchoolClassById(student.SchoolClassId);
            var DataSource = _unitofWork._GradeRepository.GetAllGradesByStudentClassYearTerm(student.Id, student.SchoolClassId).ToList();
            var studUser = _unitofWork._UserRepository.GetSingleUser(student.Id);
            foreach (var da in DataSource)
            {
                var clsName = _unitofWork._SchoolClassRepository.GetSchoolClassById(da.SchoolClassId);

                var course = _unitofWork._CourseRepository.GetCourseById(da.CourseId);
                var gSys = _unitofWork._GradeRepository.GetGradeSystemById(da.GradeSystemId);

                myResults.Add(new AcademicResultList(da.Id, studUser.FullName, da.ExamScore, da.TotalCAScore, course.Code, scclass.Code, gSys.Grade));
            }

            return Json(myResults, JsonRequestBehavior.AllowGet);
        }
    }
}