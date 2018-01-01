using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Syncfusion.JavaScript.Models;
using Syncfusion.Linq;

namespace SmpsPortal.Controllers.Academics
{
    public class ExamsController : Controller
    {
        // GET: Exams
        IUnitofWork _unitofWork;
        private List<ExamList> examList;
        private List<ExamResultList> examResultList;
        private List<ExamResultList2> examResultList2;
        private MenuRepository menuRepo;

        public ExamsController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            examList = new List<ExamList>();
            examResultList = new List<ExamResultList>();
            examResultList2 = new List<ExamResultList2>();
          menuRepo = new MenuRepository();
        }

        public ActionResult Index()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
               .Where(ck => ck.Type == ClaimTypes.Role)
               .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var teacher = _unitofWork._EmployeeRepository.GetEmployeeById(User.Identity.GetUserId());
            var courses = _unitofWork._TeacherCourseRepository.GetAllAssignedCoursesForTeacher(teacher.Id);


            var pModel = new ExamViewModel()
            {
                Menus = menus,
                Heading = "Exams",
                CourseList = courses,

            };
            ViewBag.datasource = null;
            return View(pModel);
        }
       
        public ActionResult GetDataSource(DataManager dm)
        {
            var userId = User.Identity.GetUserId();
            var teacher = _unitofWork._EmployeeRepository.GetEmployeeById(userId);
            var course = _unitofWork._TeacherCourseRepository.GetAllAssignedCoursesForTeacher(teacher.Id).First();
            var DataSource = _unitofWork._ExamRepository.GetExamsByCourse(course.Id).ToList();
            foreach (var da in DataSource)
            {
                var clsName = _unitofWork._SchoolClassRepository.GetSchoolClassById(da.SchoolClassId);
                var type = Enum.GetName(typeof(ExamType), da.Type);
                examList.Add(new ExamList(da.Id, da.Title, da.ExamNumber, type, da.Duration, clsName.Code, da.HighestScore, da.CourseId));
            }
            DataResult result = new DataResult();
            DataOperations operation = new DataOperations();
            result.result = examList;
            result.count = result.result.AsQueryable().Count();
            if (dm.Skip > 0)
                result.result = operation.PerformSkip(result.result, dm.Skip);
            if (dm.Take > 0)
                result.result = operation.PerformTake(result.result, dm.Take);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData(int courseId)
        {
            var data = _unitofWork._ExamRepository.GetExamsByCourse(courseId).ToList();
            foreach (var da in data)
            {
                var clsName = _unitofWork._SchoolClassRepository.GetSchoolClassById(da.SchoolClassId);
                var type = Enum.GetName(typeof(ExamType), da.Type);
                examList.Add(new ExamList(da.Id, da.Title, da.ExamNumber, type, da.Duration, clsName.Code, da.HighestScore, da.CourseId));
            }
            return Json(examList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExamDelete(int key)
        {
            var exam = _unitofWork._ExamRepository.GetExamById(key);
            var examResult = _unitofWork._ExamResultRepository.GetExamResultsByExamId(key);
            if (examResult.Any())
            {
                foreach (var cas in examResult)
                {
                    _unitofWork._ExamResultRepository.Remove(cas);
                    _unitofWork.Complete();
                }
            }

            _unitofWork._ExamRepository.Remove(exam);
            _unitofWork.Complete();
            var userId = User.Identity.GetUserId();
            var teacher = _unitofWork._EmployeeRepository.GetEmployeeById(userId);
            var course = _unitofWork._TeacherCourseRepository.GetAllAssignedCoursesForTeacher(teacher.Id).First();
            var DataSource = _unitofWork._ExamRepository.GetExamsByCourse(course.Id);
            foreach (var da in DataSource)
            {
                var clsName = _unitofWork._SchoolClassRepository.GetSchoolClassById(da.SchoolClassId);
                var type = Enum.GetName(typeof(ExamType), da.Type);
                examList.Add(new ExamList(da.Id, da.Title, da.ExamNumber, type, da.Duration, clsName.Code, da.HighestScore, da.CourseId));
            }

            return Json(examList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
               .Where(ck => ck.Type == ClaimTypes.Role)
               .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var teacher = _unitofWork._EmployeeRepository.GetEmployeeById(userId);
            var courses = _unitofWork._TeacherCourseRepository.GetAllAssignedCoursesForTeacher(teacher.Id);
            DateTimePickerProperties datemodel = new DateTimePickerProperties();
            datemodel.DateTimeFormat = "MM/dd/yyyy h: mm tt";
            ViewData["date"] = datemodel;
            var invigilators = _unitofWork._EmployeeRepository.GetAllActiveEmployees();
           
            var pModel = new ExamViewModel()
            {
                Menus = menus,
                Heading = "Create Exams",
                CourseList = courses,
               

            };
            return View("ExamForm", pModel);
        }

        public ActionResult ViewScoresPre(int id)
        {
            TempData["ExamId"] = id;

            return RedirectToAction("ViewScores");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExamViewModel viewModel)
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var userId = User.Identity.GetUserId();
           
            var teacher = _unitofWork._EmployeeRepository.GetEmployeeById(userId);
            var courses = _unitofWork._TeacherCourseRepository.GetAllAssignedCoursesForTeacher(teacher.Id);
            DateTimePickerProperties datemodel = new DateTimePickerProperties();
            datemodel.DateTimeFormat = "MM/dd/yyyy h: mm tt";
            ViewData["date"] = datemodel;
          

            if (!ModelState.IsValid)
            {
                viewModel.CourseList = courses;
                viewModel.Heading = "Add Exam";
                viewModel.Menus = menus;
              

                return View("ExamForm", viewModel);
            }
           
            var school = _unitofWork._SchoolClassRepository.GetSchoolConfigByWebAddress("http://localhost");
            var sc = _unitofWork._CourseRepository.GetCourseById(viewModel.CourseId);
            var emp = _unitofWork._EmployeeRepository.GetEmployeeById(userId);
            var students =
                _unitofWork._StudentRepository.GetAllActiveStudentsByClass((int)sc.SchoolClassId)
                   .ToList();
            var randomNumber = _unitofWork._EmployeeRepository.Get8Digits();
            var examNumber = sc.Code + viewModel.DateGiven.Year.ToString() + viewModel.DateGiven.Month.ToString() + randomNumber;
            var exam = new Exam()
            {
                Title = viewModel.Title,
                Answers = viewModel.Answers,
                ExamNumber = examNumber,
                CourseId = viewModel.CourseId,
                Duration = viewModel.Duration,
                DateGiven = viewModel.DateGiven,
                HighestScore = viewModel.HighestScore,
                Questions = viewModel.Questions,
                Type = viewModel.Type,
                YearTermId = school.CurrentYearTermId,
                SchoolClassId = (int)sc.SchoolClassId,
                TeacherId = emp.Id,

            };

            var examExists = _unitofWork._ExamRepository.CheckExamExistsByCourseClass(viewModel.CourseId, (int)sc.SchoolClassId);
            if (!examExists)
            {
                _unitofWork._ExamRepository.Add(exam);
                _unitofWork.Complete();

               
                var course = _unitofWork._CourseRepository.GetCourseById(viewModel.CourseId);
                IEnumerable<ApplicationUser> students2 = null;

                if (course.Category == CourseCategory.Compulsory)
                {
                    students2 = _unitofWork._StudentRepository.GetAllActiveStudentsByClass((int)sc.SchoolClassId)
                        .ToList();
                }
                else
                {
                    students2 = _unitofWork._CourseRegisterRepository.GetStudentsByCourseClass(viewModel.CourseId, (int)sc.SchoolClassId);
                }

                foreach (var stUser in students2)
                {
                    var student = _unitofWork._StudentRepository.GetStudentById(stUser.Id);
                    var examRes = new ExamResult()
                    {
                        StudentId = student.Id,
                        ExamId = exam.Id,
                        Score = 0

                    };
                    var resExists = _unitofWork._ExamResultRepository.ExamResultByStudentExamIdExists(exam.Id, student.Id);
                    if (!resExists)
                    {
                        _unitofWork._ExamResultRepository.Add(examRes);
                        _unitofWork.Complete();
                    }
                }
            }
            //var client = new TwilioRestClient(Environment.GetEnvironmentVariable("AC9ae059d5e62348d7ba71f5c3d1e8d7c0"), Environment.GetEnvironmentVariable("c64a49eea8d2a593bfdcddc11524644a"));




            return RedirectToAction("Index", "Exams");
        }
        public ActionResult GetData2(DataManager dm)
        {
            int examId = Convert.ToInt32(TempData["ExamId"]);
            var DataSource = _unitofWork._ExamResultRepository.GetExamResultsByExamId(examId);
            foreach (var da in DataSource)
            {
                var student = _unitofWork._StudentRepository.GetStudentById(da.StudentId);
                var studUser = _unitofWork._UserRepository.GetSingleUser(student.Id);

                examResultList.Add(new ExamResultList(da.Id, studUser.FullName, da.Score, da.Remarks, examId));
            }
            DataResult result = new DataResult();
            DataOperations operation = new DataOperations();
            result.result = examResultList;
            result.count = result.result.AsQueryable().Count();
            if (dm.Skip > 0)
                result.result = operation.PerformSkip(result.result, dm.Skip);
            if (dm.Take > 0)
                result.result = operation.PerformTake(result.result, dm.Take);
            TempData["ExamId"] = examId;
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetData4(DataManager dm)
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var student = _unitofWork._StudentRepository.GetStudentById(User.Identity.GetUserId());
            var courses = _unitofWork._CourseRepository.GetAllCoursesForStudent(student.Id, student.SchoolClassId);
            var course = courses.First();
            var DataSource = _unitofWork._ExamRepository.GetExamsByCourse(course.Id).ToList();
            foreach (var da in DataSource)
            {
                var type = Enum.GetName(typeof(ExamType), da.Type);
                var studRes = _unitofWork._ExamResultRepository.GetExamResultByExamStudent(da.Id, student.Id);
                examResultList2.Add(new ExamResultList2(da.Id, da.Title, type, studRes.Score, studRes.Remarks, da.HighestScore, da.CourseId));
            }
            DataResult result = new DataResult();
            DataOperations operation = new DataOperations();
            result.result = examResultList2;
            result.count = result.result.AsQueryable().Count();
            if (dm.Skip > 0)
                result.result = operation.PerformSkip(result.result, dm.Skip);
            if (dm.Take > 0)
                result.result = operation.PerformTake(result.result, dm.Take);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult StudentExam()
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
          
            var student = _unitofWork._StudentRepository.GetStudentById(userId);
            var mCourses = _unitofWork._CourseRepository.GetAllCoursesForStudent(student.Id, student.SchoolClassId);


            var pModel = new ExamViewModel()
            {
                Menus = menus,
                Heading = "My Exams",
                CourseList = mCourses
            };
            ViewBag.datasource = null;

            return View("StudentView", pModel);
        }
        public JsonResult GetData3(int courseId)
        {
            var data = _unitofWork._ExamRepository.GetExamsByCourse(courseId);
            var userId = User.Identity.GetUserId();

            var student = _unitofWork._StudentRepository.GetStudentById(userId);
            foreach (var da in data)
            {
                var type = Enum.GetName(typeof(ExamType), da.Type);
                var studRes = _unitofWork._ExamResultRepository.GetExamResultByExamStudent(da.Id, student.Id);
                examResultList2.Add(new ExamResultList2(da.Id, da.Title, type, studRes.Score, studRes.Remarks, da.HighestScore, da.CourseId));
            }
            return Json(examResultList2, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExUpdate(ExamResultList value)
        {

            var examRes = _unitofWork._ExamResultRepository.GetExamResultById(value.Id);
            examRes.Score = Convert.ToDouble(value.Score);
            examRes.Remarks = value.Remark;
            int examId = Convert.ToInt32(TempData["ExamId"]);
            _unitofWork.Complete();
            var DataSource = _unitofWork._ExamResultRepository.GetExamResultsByExamId(examId);
            foreach (var da in DataSource)
            {
                var student = _unitofWork._StudentRepository.GetStudentById(da.StudentId);
                var studUser = _unitofWork._UserRepository.GetSingleUser(student.Id);

                examResultList.Add(new ExamResultList(da.Id, studUser.FullName, da.Score, da.Remarks, examId));
            }
            TempData["ExamId"] = examId;
            return Json(examResultList, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult ViewScores()
        {
            var eModel = new ExamResultViewModel();
            int cid = 0;

            cid = Convert.ToInt32(TempData["ExamId"]);
            eModel = ScoreUp(cid);

            var userId = User.Identity.GetUserId();

            ViewBag.datasource = null;
            return View(eModel);
        }
        public ExamResultViewModel ScoreUp(int id)
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                          .Where(ck => ck.Type == ClaimTypes.Role)
                          .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var exam = _unitofWork._ExamRepository.GetExamById((int)id);
            var results = _unitofWork._ExamResultRepository.GetExamResultsByExamId((int)id);
            var crs = _unitofWork._CourseRepository.GetCourseById(exam.CourseId);
            var cls = _unitofWork._SchoolClassRepository.GetSchoolClassById(exam.SchoolClassId);
            var cModel = new ExamResultViewModel()
            {
                ExamId = (int)id,
                Title = exam.Title,
                DateGiven = exam.DateGiven,
                HighestScore = exam.HighestScore,
                Heading = "Score Exams",
                Menus = menus,
                CourseName = crs.Code,
                ClassName = cls.Code
            };
            TempData["ExamId"] = (int)id;

            return cModel;
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
           
            var teacher = _unitofWork._EmployeeRepository.GetEmployeeById(userId);
            var courses = _unitofWork._TeacherCourseRepository.GetAllAssignedCoursesForTeacher(teacher.Id);
            var exam = _unitofWork._ExamRepository.GetExamById(id);
            DateTimePickerProperties datemodel = new DateTimePickerProperties();
            datemodel.DateTimeFormat = "MM/dd/yyyy h: mm tt";
            ViewData["date"] = datemodel;
           
         
            var pModel = new ExamViewModel()
            {
                Menus = menus,
                Heading = "Edit Exam",
                CourseList = courses,
                CourseId = exam.CourseId,
                Id = id,
                Duration = exam.Duration,
                Type = exam.Type,
                Answers = WebUtility.HtmlDecode(exam.Answers),
                Questions = WebUtility.HtmlDecode(exam.Questions),
                DateGiven = exam.DateGiven,
                HighestScore = exam.HighestScore,
                Title = exam.Title
               
            };

            return View("ExamForm", pModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Update(ExamViewModel viewModel)
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var userId = User.Identity.GetUserId();
            
            var teacher = _unitofWork._EmployeeRepository.GetEmployeeById(userId);
            var courses = _unitofWork._TeacherCourseRepository.GetAllAssignedCoursesForTeacher(teacher.Id);
            DateTimePickerProperties datemodel = new DateTimePickerProperties();
            datemodel.DateTimeFormat = "MM/dd/yyyy h: mm tt";
            ViewData["date"] = datemodel;
           

            if (!ModelState.IsValid)
            {
                viewModel.CourseList = courses;
                viewModel.Heading = "Edit Exam";
                viewModel.Menus = menus;
            

                return View("ExamForm", viewModel);
            }


            var exam = _unitofWork._ExamRepository.GetExamById(viewModel.Id);

            exam.Modify(viewModel.Title, viewModel.Questions, viewModel.Answers, viewModel.Duration, viewModel.HighestScore, viewModel.Type, viewModel.DateGiven);
            _unitofWork.Complete();
         

            var sc = _unitofWork._CourseRepository.GetCourseById(viewModel.CourseId);
            var students =
                _unitofWork._StudentRepository.GetAllActiveStudentsByClass((int)sc.SchoolClassId)
                   .ToList();

            var course = _unitofWork._CourseRepository.GetCourseById(viewModel.CourseId);
            IEnumerable<ApplicationUser> students2 = null;

            if (course.Category == CourseCategory.Compulsory)
            {
                students2 = _unitofWork._StudentRepository.GetAllActiveStudentsByClass((int)sc.SchoolClassId)
                   .ToList();
            }
            else
            {
                students2 = _unitofWork._CourseRegisterRepository.GetStudentsByCourseClass(viewModel.CourseId, (int)sc.SchoolClassId);
            }

            foreach (var stUser in students2)
            {
                var student = _unitofWork._StudentRepository.GetStudentById(stUser.Id);
                var examRes = new ExamResult()
                {
                    StudentId = student.Id,
                    ExamId = exam.Id,
                    Score = 0

                };
                var resExists = _unitofWork._ExamResultRepository.ExamResultByStudentExamIdExists(exam.Id, student.Id);
                if (!resExists)
                {
                    _unitofWork._ExamResultRepository.Add(examRes);
                    _unitofWork.Complete();
                }
            }
            return RedirectToAction("Index", "Exams");
        }
    }
}