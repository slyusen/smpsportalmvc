using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SmpsPortal.Core;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.ViewModels;
using SmpsPortal.Persistence;
using SmpsPortal.Persistence.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Syncfusion.JavaScript.Models;

namespace SmpsPortal.Controllers.Administration
{
    public class StudentsController : Controller
    {
        private ApplicationUserManager _userManager;
        public IUnitofWork _unitofWork;
        private List<StudentList> studList;
        private MenuRepository menuRepo;

        public StudentsController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            studList = new List<StudentList>();
            menuRepo = new MenuRepository();
        }
        public StudentsController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Employee
        public ActionResult Index()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
               .Where(ck => ck.Type == ClaimTypes.Role)
               .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var pModel = new StudentViewModel()
            {
                Menus = menus,
                Heading = "Students"
            };
            var students = _unitofWork._StudentRepository.GetAllActiveStudents();
            foreach (var stu in students)
            {
                var gLevel = _unitofWork._GradeLevelRepository.GetGradeLevelById(stu.GradeLevelId);
                var sClass = _unitofWork._SchoolClassRepository.GetSchoolClassById(stu.SchoolClassId);
                
                
                studList.Add(new StudentList(stu.Id, stu.StudentNumber, stu.ProfileImageUrl, stu.FullName, stu.Email, stu.PhoneNumber, gLevel.Code, sClass.Code));

            }
            ViewBag.datasource = studList;

            return View(pModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var gradelist = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
            var pModel = new StudentViewModel()
            {
                Menus = menus,
                GradeLevelList = gradelist,
                Heading = "Add new Student"
            };

            return View(pModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();


                var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
                var gradelist = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
                var pModel = new StudentViewModel()
                {
                    Menus = menus,
                    GradeLevelList = gradelist,
                    Heading = "Add new Student"
                };

                return View("Create", pModel);
            }
             ApplicationUser user = null;



                UserStore userStore = new StudentStore();
                user = userStore.createUser(UserType.Student, model);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.DateofBirth = model.DateofBirth;
                user.PhoneNumber = model.Phone;
                user.Gender = model.Gender;
                user.ProfileImageUrl = "avatar.png";
                user.Email = model.Email;
                user.UserName = model.Email;



            var pass = "Smps321#";

            if (model.Password != "")
                pass = model.Password;
            var uMan = UserManager;
            IdentityResult result = new IdentityResult();
            try
            {
                result = uMan.Create(user, pass);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            if (result.Succeeded)
            {
                UserManager.AddToRole(user.Id, "Student");

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                var medRecord = new MedicalRecord()
                {
                    AppUserId = user.Id,
                    BloodGroup = model.BloodGroup,
                    Genotype = model.Genotype,
                    Allergies = model.Allergies,
                    BriefMedicalHistory = model.BriefMedicalHistory,
                    Hmo = model.Hmo,
                    HmoNumber = model.HmoNumber,
                    Medications = model.Medications,
                    PhysicianName = model.PhysicianName,
                    PhysicianPhone = model.PhysicianPhone
                };

                _unitofWork._UserRepository.AddMedicalRecord(medRecord);
                _unitofWork.Complete();
                return RedirectToAction("Index", "Students");
                }
                AddErrors(result);

           
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize]
        public ActionResult Edit(string id)
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var gradelist = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
           
            DatePickerProperties datemodel = new DatePickerProperties();
            datemodel.DateFormat = "MM/dd/yyyy";
            ViewData["date"] = datemodel;

            var student = _unitofWork._StudentRepository.GetStudentById(id);

            var schoolClass =
                _unitofWork._SchoolClassRepository.GetSchoolClassByGradeLevel((int)student.GradeLevelId);
            var model = new StudentViewModel();
            model.Id = id;
            model.DateEnrolled = student.DateEnrolled;
            model.Heading = "Edit Student";
            model.Menus = menus;
            model.GradeLevelList = gradelist;
            model.SchoolClassList = schoolClass;
            model.Address = student.Address;
            model.DateofBirth = student.DateofBirth;
            model.Gender = student.Gender;
            model.Email = student.Email;
            model.FirstName = student.FirstName;
            model.LastName = student.LastName;
            model.IsActive = student.IsActive;
            model.UserType = UserType.Student;
            model.Phone = student.PhoneNumber;
            model.GradeLevelId = student.GradeLevelId;
            model.SchoolClassId = student.SchoolClassId;

            var medRecord = _unitofWork._UserRepository.GetMedicalRecordByUserId(id);
            if (medRecord != null)
            {
                model.Hmo = medRecord.Hmo;
                model.HmoNumber = medRecord.HmoNumber;
                model.Medications = medRecord.Medications;
                model.PhysicianName = medRecord.PhysicianName;
                model.PhysicianPhone = medRecord.PhysicianPhone;
                model.Allergies = medRecord.Allergies;
                model.BloodGroup = medRecord.BloodGroup;
                model.BriefMedicalHistory = medRecord.BriefMedicalHistory;
                model.Genotype = medRecord.Genotype;
            }
            


            return View("Edit", model);

        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(StudentViewModel model)
        {


            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var gradelist = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();

            DatePickerProperties datemodel = new DatePickerProperties();
            datemodel.DateFormat = "MM/dd/yyyy";
            ViewData["date"] = datemodel;

            var student = _unitofWork._StudentRepository.GetStudentById(model.Id);

            var schoolClass =
                _unitofWork._SchoolClassRepository.GetSchoolClassByGradeLevel((int)student.GradeLevelId);

            if (!ModelState.IsValid)
            {
                var viewModel = new StudentViewModel()
                {
                    SchoolClassList = schoolClass,
                    Menus = menus,
                    GradeLevelList = gradelist,
                    Heading = "Add new Student"
                };
                return View("Edit", viewModel);
            }


            student.DateEnrolled = model.DateEnrolled;
            student.GradeLevelId = model.GradeLevelId;
            student.SchoolClassId = model.SchoolClassId;
            student.Address = model.Address;
            student.DateofBirth = model.DateofBirth;
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            if (model.IsActive)
                student.ReActivate();
            else if (!model.IsActive)
            {
                student.Deactivate();
            }
            student.PhoneNumber = model.Phone;

            _unitofWork.Complete();

            var medRecord = _unitofWork._UserRepository.GetMedicalRecordByUserId(model.Id);

            medRecord.Modify(model.BloodGroup, model.Genotype, model.PhysicianName,
                model.PhysicianPhone, model.Hmo, model.HmoNumber, model.Allergies,
                model.Medications, model.BriefMedicalHistory);

            _unitofWork.Complete();
            return RedirectToAction("Index", "Students");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}