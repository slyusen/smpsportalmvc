using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
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
    public class AssessmentController : Controller
    {
        private IUnitofWork _unitofWork;
        private List<CaList> myCaList;
        private List<CaListItems> myCaListItems;
        private List<CaResultList> myCaResults;
        private List<StudentList> studList;
        private MenuRepository menuRepo;

        public AssessmentController()
        {
            menuRepo = new MenuRepository();
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            myCaList = new List<CaList>();
            myCaResults = new List<CaResultList>();
            studList = new List<StudentList>();
            myCaListItems = new List<CaListItems>();
        }
        public ActionResult Index()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
               .Where(ck => ck.Type == ClaimTypes.Role)
               .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var teacher = _unitofWork._EmployeeRepository.GetEmployeeById(User.Identity.GetUserId());
            var courses = _unitofWork._TeacherCourseRepository.GetAllAssignedCoursesForTeacher(teacher.Id);
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "Assignment", Value = "Assignment" });
            listItem.Add(new SelectListItem { Text = "Test", Value = "Test" });
            listItem.Add(new SelectListItem { Text = "Practical", Value = "Practical" });

            var pModel = new AssessmentViewModel()
            {
                Menus = menus,
                Heading = "Assessments",
                CourseList = courses,
                TypeListItems = listItem

            };
            ViewBag.datasource = null;
            return View(pModel);
        }

        [Authorize]
        public ActionResult ViewResponse(int id)
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var caRes = _unitofWork._CaResultRepository.GetCaResultById(id);
            var stud = _unitofWork._StudentRepository.GetStudentById(caRes.StudentId);
            var studUser = _unitofWork._UserRepository.GetSingleUser(stud.Id);
            var cAssessment = _unitofWork._AssessmentRepository.GetContinuousAssessmentById(caRes.AssessmentId);
            var course = _unitofWork._CourseRepository.GetCourseById(cAssessment.CourseId);

            var caModel = new CaResponseViewModel()
            {
                Id = id,
                Menus = menus,
                Heading = "Student Response to Assessment",
                Title = cAssessment.Title + " for - " + course.Code,
                StudentName = studUser.FullName,
                Score = caRes.Score,
                Remark = caRes.Remark,
                Response = WebUtility.HtmlDecode(caRes.Response),
                Questions = WebUtility.HtmlDecode(cAssessment.Questions)

            };
            return View(caModel);
        }

        [Authorize]
        public ActionResult StudentCa()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
             .Where(ck => ck.Type == ClaimTypes.Role)
             .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var student = _unitofWork._StudentRepository.GetStudentById(User.Identity.GetUserId());
            var mCourses = _unitofWork._CourseRepository.GetAllCoursesForStudent(student.Id, student.SchoolClassId);
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "Assignment", Value = "Assignment" });
            listItem.Add(new SelectListItem { Text = "Test", Value = "Test" });
            listItem.Add(new SelectListItem { Text = "Practical", Value = "Practical" });

            var pModel = new AssessmentViewModel()
            {
                Menus = menus,
                Heading = "My Assessments",
                CourseList = mCourses,
                TypeListItems = listItem

            };
            ViewBag.datasource = null;

            return View("StudentCAView", pModel);
        }

        [Authorize]
        public ActionResult ViewAssessments()
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
             .Where(ck => ck.Type == ClaimTypes.Role)
             .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var parent = _unitofWork._ParentRepository.GetParentById(userId);
            var students = _unitofWork._StudentParentRepository.GetAllStudentParentsByParent(parent.Id).Select(p => p.Student);

            foreach (var student in students)
            {
                var stUser = _unitofWork._UserRepository.GetSingleUser(student.Id);
                studList.Add(new StudentList(student.Id, stUser.FullName));
            }
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "Assignment", Value = "Assignment" });
            listItem.Add(new SelectListItem { Text = "Test", Value = "Test" });
            listItem.Add(new SelectListItem { Text = "Practical", Value = "Practical" });

            var pModel = new AssessmentViewModel()
            {
                Menus = menus,
                Heading = "Student Assessments",
                TypeListItems = listItem,
                StudentList = studList
            };
            ViewBag.datasource = null;

            return View(pModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ResponseUpdate(CaResponseViewModel cModel)
        {
            var caResult = _unitofWork._CaResultRepository.GetCaResultById(cModel.Id);

            caResult.Score = cModel.Score;
            caResult.Remark = cModel.Remark;

            _unitofWork.Complete();
            TempData["CaId"] = caResult.AssessmentId;
            return RedirectToAction("ViewScores");


        }
        public ActionResult GetDataSource(DataManager dm)
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
             .Where(ck => ck.Type == ClaimTypes.Role)
             .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var teacher = _unitofWork._EmployeeRepository.GetEmployeeById(userId);

            var tcourse = _unitofWork._TeacherCourseRepository.GetAllAssignedCoursesForTeacher(teacher.Id);
            Course course = null;
            if (tcourse != null)
            {
                course = tcourse.First();
                var DataSource = _unitofWork._AssessmentRepository.GetAssessmentsByCourse(course.Id).ToList();
                foreach (var da in DataSource)
                {
                    var clsName = _unitofWork._SchoolClassRepository.GetSchoolClassById(da.SchoolClassId);

                    myCaList.Add(new CaList(da.Id, da.Title, EnumToString.StringValue(da.Type), clsName.Code,
                        da.HighestScore, da.CourseId));
                }
                DataResult result = new DataResult();
                DataOperations operation = new DataOperations();
                result.result = myCaList;
                result.count = result.result.AsQueryable().Count();
                if (dm.Skip > 0)
                    result.result = operation.PerformSkip(result.result, dm.Skip);
                if (dm.Take > 0)
                    result.result = operation.PerformTake(result.result, dm.Take);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("");
            }
        }

        public ActionResult GetData4(DataManager dm)
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
           .Where(ck => ck.Type == ClaimTypes.Role)
           .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var student = _unitofWork._StudentRepository.GetStudentById(userId);
            var courses = _unitofWork._CourseRepository.GetAllCoursesByClass(student.SchoolClassId);
            var course = courses.First();
            var DataSource = _unitofWork._AssessmentRepository.GetAssessmentsByCourse(course.Id).ToList();
            foreach (var da in DataSource)
            {
                var resExists = _unitofWork._CaResultRepository.CaResultByStudentCaIdExists(da.Id, student.Id);
                if (resExists)
                {
                    var studRes = _unitofWork._CaResultRepository.GetCaResultByCaStudent(da.Id, student.Id);
                    myCaList.Add(new CaList(da.Id, da.Title, EnumToString.StringValue(da.Type), studRes.Score, studRes.Remark, da.HighestScore, da.CourseId));
                }
            }
            DataResult result = new DataResult();
            DataOperations operation = new DataOperations();
            result.result = myCaList;
            result.count = result.result.AsQueryable().Count();
            if (dm.Skip > 0)
                result.result = operation.PerformSkip(result.result, dm.Skip);
            if (dm.Take > 0)
                result.result = operation.PerformTake(result.result, dm.Take);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetData5(DataManager dm)
        {
           
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
           .Where(ck => ck.Type == ClaimTypes.Role)
           .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var parent = _unitofWork._ParentRepository.GetParentById(userId);
            var students = _unitofWork._StudentParentRepository.GetAllStudentParentsByParent(parent.Id).Select(p => p.Student);
            var student = students.FirstOrDefault();

            var DataSource = _unitofWork._AssessmentRepository.GetAssessmentsByCourse(student.SchoolClassId).ToList();
            foreach (var da in DataSource)
            {
                var resExists = _unitofWork._CaResultRepository.CaResultByStudentCaIdExists(da.Id, student.Id);
                if (resExists)
                {
                    var studRes = _unitofWork._CaResultRepository.GetCaResultByCaStudent(da.Id, student.Id);
                    var course = _unitofWork._CourseRepository.GetCourseById(da.CourseId);
                    myCaListItems.Add(new CaListItems(da.Id, da.Title, EnumToString.StringValue(da.Type), studRes.Score, studRes.Remark, da.HighestScore, da.CourseId, course.Code));
                }
            }
            DataResult result = new DataResult();
            DataOperations operation = new DataOperations();
            result.result = myCaListItems;
            result.count = result.result.AsQueryable().Count();
            if (dm.Skip > 0)
                result.result = operation.PerformSkip(result.result, dm.Skip);
            if (dm.Take > 0)
                result.result = operation.PerformTake(result.result, dm.Take);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CaUpdate(CaResultList value)
        {

            var caRes = _unitofWork._CaResultRepository.GetCaResultById(value.Id);
            caRes.Score = Convert.ToDouble(value.Score);
            caRes.Remark = value.Remark;
            int caId = Convert.ToInt32(TempData["CaId"]);
            _unitofWork.Complete();
            var DataSource = _unitofWork._CaResultRepository.GetCaResultsByCaId(caId);
            foreach (var da in DataSource)
            {
                var student = _unitofWork._StudentRepository.GetStudentById(da.StudentId);
                var studUser = _unitofWork._UserRepository.GetSingleUser(student.Id);
                myCaResults.Add(new CaResultList(da.Id, studUser.FullName, da.Score, da.DateSubmitted, caId, da.Remark));
            }
            TempData["CaId"] = caId;
            return Json(myCaResults, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetData(int courseId)
        {
            var data = _unitofWork._AssessmentRepository.GetAssessmentsByCourse(courseId);
            foreach (var da in data)
            {
                var clsName = _unitofWork._SchoolClassRepository.GetSchoolClassById(da.SchoolClassId);

                myCaList.Add(new CaList(da.Id, da.Title, EnumToString.StringValue(da.Type), clsName.Code, da.HighestScore, da.CourseId));
            }
            return Json(myCaList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetData3(int courseId)
        {
            var data = _unitofWork._AssessmentRepository.GetAssessmentsByCourse(courseId);
            var userId = User.Identity.GetUserId();

            var student = _unitofWork._StudentRepository.GetStudentById(userId);
            foreach (var da in data)
            {
                var studRes = _unitofWork._CaResultRepository.GetCaResultByCaStudent(da.Id, student.Id);
                myCaList.Add(new CaList(da.Id, da.Title, EnumToString.StringValue(da.Type), studRes.Score, studRes.Remark, da.HighestScore, da.CourseId));
            }
            return Json(myCaList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData6(string studentId)
        {
            var student = _unitofWork._StudentRepository.GetStudentById(studentId);
            var data = _unitofWork._AssessmentRepository.GetAssessmentByClass(student.SchoolClassId);
            var userId = User.Identity.GetUserId();

            foreach (var da in data)
            {
                var resExists = _unitofWork._CaResultRepository.CaResultByStudentCaIdExists(da.Id, student.Id);
                if (resExists)
                {
                    var studRes = _unitofWork._CaResultRepository.GetCaResultByCaStudent(da.Id, student.Id);
                    var course = _unitofWork._CourseRepository.GetCourseById(da.CourseId);
                    myCaListItems.Add(new CaListItems(da.Id, da.Title, EnumToString.StringValue(da.Type), studRes.Score, studRes.Remark, da.HighestScore, da.CourseId, course.Code));
                }
            }
            return Json(myCaListItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudResponse(int id)
        {
            TempData["CId"] = id;

            return RedirectToAction("StudentResponse");
        }
        public ActionResult StudentResponse()
        {
          
            var roleList = ((ClaimsIdentity)User.Identity).Claims
           .Where(ck => ck.Type == ClaimTypes.Role)
           .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            int id = Convert.ToInt32(TempData["CId"]);
            var userId = User.Identity.GetUserId();
          
            var stud = _unitofWork._StudentRepository.GetStudentById(userId);
            var caRes = _unitofWork._CaResultRepository.GetCaResultByCaStudent(id, stud.Id);
            var studUser = _unitofWork._UserRepository.GetSingleUser(userId);
            var cAssessment = _unitofWork._AssessmentRepository.GetContinuousAssessmentById(caRes.AssessmentId);
            var course = _unitofWork._CourseRepository.GetCourseById(cAssessment.CourseId);

            var caModel = new CaResponseViewModel()
            {
                Id = caRes.Id,
                Menus = menus,
                Heading = "Respond to Assessment",
                Title = cAssessment.Title + " for - " + course.Code,
                StudentName = studUser.FullName,
                Score = caRes.Score,
                Remark = caRes.Remark,
                DateDue = cAssessment.DateDue,
                Response = WebUtility.HtmlDecode(caRes.Response),
                Questions = WebUtility.HtmlDecode(cAssessment.Questions),
                ActionChosen = "Student"

            };
            TempData["CId"] = id;
            TempData["CaResId"] = caRes.Id;
            return View("StudResponse", caModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult StudResponseUpdate(CaResponseViewModel viewModel)
        {
            int caResId = Convert.ToInt32(TempData["CaResId"]);
            var caResult = _unitofWork._CaResultRepository.GetCaResultById(caResId);
            var ca = _unitofWork._AssessmentRepository.GetContinuousAssessmentById(caResult.AssessmentId);
            
            var roleList = ((ClaimsIdentity)User.Identity).Claims
           .Where(ck => ck.Type == ClaimTypes.Role)
           .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            if (!ModelState.IsValid)
            {
                viewModel.Menus = menus;

                return View("StudResponse", viewModel);
            }

            if (ca.DateDue > DateTime.Now)
            {
                caResult.Response = viewModel.Response;
                caResult.DateSubmitted = DateTime.Now;

                _unitofWork.Complete();
            }
            return RedirectToAction("StudentCa");
        }
       

        [Authorize]
        public ActionResult Create()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
         .Where(ck => ck.Type == ClaimTypes.Role)
         .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var teacher = _unitofWork._EmployeeRepository.GetEmployeeById(User.Identity.GetUserId());
            var courses = _unitofWork._TeacherCourseRepository.GetAllAssignedCoursesForTeacher(teacher.Id);
            DateTimePickerProperties datemodel = new DateTimePickerProperties();
            datemodel.DateTimeFormat = "MM/dd/yyyy h: mm tt";
            ViewData["date"] = datemodel;
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "Assignment", Value = "Assignment" });
            listItem.Add(new SelectListItem { Text = "Test", Value = "Test" });
            listItem.Add(new SelectListItem { Text = "Practical", Value = "Practical" });
            var pModel = new AssessmentViewModel()
            {
                Menus = menus,
                Heading = "Create Assessment",
                CourseList = courses,
                TypeListItems = listItem

            };
            return View("CAForm", pModel);
        }

        public ActionResult ViewScoresPre(int id)
        {
            TempData["CaId"] = id;

            return RedirectToAction("ViewScores");
        }

        [Authorize]
        public ActionResult ViewScores()
        {
            var cModel = new CaResultViewModel();
            int cid = 0;

            cid = Convert.ToInt32(TempData["CaId"]);
            cModel = ScoreUp(cid);


            var userId = User.Identity.GetUserId();


            ViewBag.datasource = null;
            return View(cModel);
        }
        public CaResultViewModel ScoreUp(int id)
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
           
            var assessment = _unitofWork._AssessmentRepository.GetContinuousAssessmentById((int)id);
            var results = _unitofWork._CaResultRepository.GetCaResultsByCaId((int)id);
            var crs = _unitofWork._CourseRepository.GetCourseById(assessment.CourseId);
            var cls = _unitofWork._SchoolClassRepository.GetSchoolClassById(assessment.SchoolClassId);
            var cModel = new CaResultViewModel()
            {
                CaId = (int)id,
                Title = assessment.Title,
                PercentofCa = assessment.PercentofCa,
                DateGiven = assessment.DateGiven,
                DateDue = assessment.DateDue,
                HighestScore = assessment.HighestScore,
                Heading = "Score Assessments",
                Menus = menus,
                CourseName = crs.Code,
                ClassName = cls.Code
            };
            TempData["CaId"] = (int)id;

            return cModel;
        }

        public ActionResult GetData2(DataManager dm)
        {
            int CaID = Convert.ToInt32(TempData["CaId"]);
            var DataSource = _unitofWork._CaResultRepository.GetCaResultsByCaId(CaID);
            foreach (var da in DataSource)
            {
                var student = _unitofWork._StudentRepository.GetStudentById(da.StudentId);
               

                myCaResults.Add(new CaResultList(da.Id, student.FullName, da.Score, da.DateSubmitted, CaID, da.Remark));
            }
            DataResult result = new DataResult();
            DataOperations operation = new DataOperations();
            result.result = myCaResults;
            result.count = result.result.AsQueryable().Count();
            if (dm.Skip > 0)
                result.result = operation.PerformSkip(result.result, dm.Skip);
            if (dm.Take > 0)
                result.result = operation.PerformTake(result.result, dm.Take);
            TempData["CaId"] = CaID;
            return Json(result, JsonRequestBehavior.AllowGet);

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
            var ca = _unitofWork._AssessmentRepository.GetContinuousAssessmentById(id);
            DateTimePickerProperties datemodel = new DateTimePickerProperties();
            datemodel.DateTimeFormat = "MM/dd/yyyy h: mm tt";
            ViewData["date"] = datemodel;
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "Assignment", Value = "Assignment" });
            listItem.Add(new SelectListItem { Text = "Test", Value = "Test" });
            listItem.Add(new SelectListItem { Text = "Practical", Value = "Practical" });
            var pModel = new AssessmentViewModel()
            {
                Menus = menus,
                Heading = "Edit Assessment",
                CourseList = courses,
                TypeListItems = listItem,
                CourseId = ca.CourseId,
                Id = id,
                Answers = WebUtility.HtmlDecode(ca.Answers),
                Questions = WebUtility.HtmlDecode(ca.Questions),
                DateDue = ca.DateDue,
                DateGiven = ca.DateGiven,
                PercentofCa = ca.PercentofCa,
                HighestScore = ca.HighestScore,
                Title = ca.Title,
                IsChosen = ca.IsChosen

            };

            return View("CAForm", pModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssessmentViewModel viewModel)
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
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "Assignment", Value = "Assignment" });
            listItem.Add(new SelectListItem { Text = "Test", Value = "Test" });
            listItem.Add(new SelectListItem { Text = "Practical", Value = "Practical" });


            if (!ModelState.IsValid)
            {
                viewModel.CourseList = courses;
                viewModel.Heading = "Add Assessment";
                viewModel.Menus = menus;
                viewModel.TypeListItems = listItem;
                return View("CAForm", viewModel);
            }
           
           
            var sc = _unitofWork._CourseRepository.GetCourseById(viewModel.CourseId);
            var school = _unitofWork._SchoolClassRepository.GetSchoolConfigByWebAddress("http://localhost");
            var emp = _unitofWork._EmployeeRepository.GetEmployeeById(userId);
            var students =
                _unitofWork._StudentRepository.GetAllActiveStudentsByClass((int)sc.SchoolClassId)
                    .ToList();

            var ca = new Assessment()
            {
                Title = viewModel.Title,
                Answers = viewModel.Answers,
                CourseId = viewModel.CourseId,
                DateDue = viewModel.DateDue,
                DateGiven = DateTime.Now,
                HighestScore = viewModel.HighestScore,
                PercentofCa = viewModel.PercentofCa,
                Questions = viewModel.Questions,
                Type =  (AssessmentType)Enum.Parse(typeof(AssessmentType), viewModel.Type),
                IsChosen = viewModel.IsChosen,
                YearTermId = school.CurrentYearTermId,
                SchoolClassId = (int)sc.SchoolClassId,
                TeacherId = emp.Id,

            };


            _unitofWork._AssessmentRepository.Add(ca);
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
            //var client = new TwilioRestClient(Environment.GetEnvironmentVariable("AC9ae059d5e62348d7ba71f5c3d1e8d7c0"), Environment.GetEnvironmentVariable("c64a49eea8d2a593bfdcddc11524644a"));
            foreach (var usr in students2)
            {
                //client.SendMessage("+18562882944", usr.PhoneNumber, aModel.Message);
                var attending = _unitofWork._AttendanceRepository.IsUserAssessment(usr.Id, ca.Id);
                if (!attending)
                {
                    var attend = new Attendance()
                    {
                        AttendeeId = usr.Id,
                        AssessmentId = ca.Id
                    };
                    _unitofWork._AttendanceRepository.Add(attend);
                    _unitofWork.Complete();
                }

            }

            ca.Attendances = _unitofWork._AttendanceRepository.GetAllAttendance().Where(a => a.AssessmentId == ca.Id).ToList();
            ca.Create();
            _unitofWork.Complete();
            foreach (var stUser in students2)
            {
                var student = _unitofWork._StudentRepository.GetStudentById(stUser.Id);
                var caRes = new CaResult()
                {
                    StudentId = student.Id,
                    AssessmentId = ca.Id,
                    Score = 0

                };
                var resExists = _unitofWork._CaResultRepository.CaResultByStudentCaIdExists(ca.Id, student.Id);
                if (!resExists)
                {
                    _unitofWork._CaResultRepository.Add(caRes);
                    _unitofWork.Complete();
                }
            }

            return RedirectToAction("Index", "Assessment");
        }

        public ActionResult CaDelete(int key)
        {
            var ca = _unitofWork._AssessmentRepository.GetContinuousAssessmentById(key);
            var caresults = _unitofWork._CaResultRepository.GetCaResultsByCaId(key);
            if (caresults.Any())
            {
                foreach (var cas in caresults)
                {
                    _unitofWork._CaResultRepository.Remove(cas);
                    _unitofWork.Complete();
                }
            }

            _unitofWork._AssessmentRepository.Remove(ca);
            _unitofWork.Complete();
            var userId = User.Identity.GetUserId();
            var teacher = _unitofWork._EmployeeRepository.GetEmployeeById(userId);
            var course = _unitofWork._TeacherCourseRepository.GetAllAssignedCoursesForTeacher(teacher.Id).First();
            var DataSource = _unitofWork._AssessmentRepository.GetAssessmentsByCourse(course.Id);
            foreach (var da in DataSource)
            {
                var clsName = _unitofWork._SchoolClassRepository.GetSchoolClassById(da.SchoolClassId);

                myCaList.Add(new CaList(da.Id, da.Title, EnumToString.StringValue(da.Type), clsName.Code, da.HighestScore, da.CourseId));
            }

            return Json(myCaList, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Update(AssessmentViewModel viewModel)
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
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "Assignment", Value = "Assignment" });
            listItem.Add(new SelectListItem { Text = "Test", Value = "Test" });
            listItem.Add(new SelectListItem { Text = "Practical", Value = "Practical" });

            if (!ModelState.IsValid)
            {
                viewModel.CourseList = courses;
                viewModel.Heading = "Edit Assessment";
                viewModel.Menus = menus;
                viewModel.TypeListItems = listItem;
                return View("CAForm", viewModel);
            }


            var ca = _unitofWork._AssessmentRepository.GetContinuousAssessmentById(viewModel.Id);

            ca.Modify(viewModel.Title, viewModel.Questions, viewModel.Answers,  (AssessmentType)Enum.Parse(typeof(AssessmentType), viewModel.Type) , viewModel.HighestScore, viewModel.PercentofCa, viewModel.DateDue, viewModel.CourseId, viewModel.IsChosen);

            _unitofWork.Complete();
            var sc = _unitofWork._CourseRepository.GetCourseById(viewModel.CourseId);
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
            //var client = new TwilioRestClient(Environment.GetEnvironmentVariable("AC9ae059d5e62348d7ba71f5c3d1e8d7c0"), Environment.GetEnvironmentVariable("c64a49eea8d2a593bfdcddc11524644a"));
            foreach (var usr in students2)
            {
                //client.SendMessage("+18562882944", usr.PhoneNumber, aModel.Message);
                var attending = _unitofWork._AttendanceRepository.IsUserAssessment(usr.Id, ca.Id);
                if (!attending)
                {
                    var attend = new Attendance()
                    {
                        AttendeeId = usr.Id,
                        AssessmentId = ca.Id
                    };
                    _unitofWork._AttendanceRepository.Add(attend);
                    _unitofWork.Complete();

                    ca.Attendances = _unitofWork._AttendanceRepository.GetAllAttendance().Where(a => a.AssessmentId == ca.Id && a.AttendeeId == usr.Id).ToList();
                    ca.Create();
                    _unitofWork.Complete();
                }

            }


            foreach (var stUser in students2)
            {
                var student = _unitofWork._StudentRepository.GetStudentById(stUser.Id);
                var caRes = new CaResult()
                {
                    StudentId = student.Id,
                    AssessmentId = ca.Id,
                    Score = 0

                };
                var resExists = _unitofWork._CaResultRepository.CaResultByStudentCaIdExists(ca.Id, student.Id);
                if (!resExists)
                {
                    _unitofWork._CaResultRepository.Add(caRes);
                    _unitofWork.Complete();
                }
            }

            return RedirectToAction("Index", "Assessment");
        }
    }
}