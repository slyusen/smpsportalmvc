using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using SmpsPortal.Core;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.ViewModels;
using SmpsPortal.Persistence;
using SmpsPortal.Persistence.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Syncfusion.JavaScript.Models;

namespace SmpsPortal.Controllers.HR
{
    public class EmployeeController : Controller
    {
        private ApplicationUserManager _userManager;

        public IUnitofWork _unitofWork;
        private List<EmployeeList> empList;
        private MenuRepository menuRepo;

        public EmployeeController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            empList = new List<EmployeeList>();
            menuRepo = new MenuRepository();
        }

        public EmployeeController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        // GET: Employee
        public ActionResult Index()
        {
            var roleList = ((ClaimsIdentity) User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);


            var pModel = new EmployeeViewModel()
            {
                Menus = menus,
                Heading = "Employees"
            };
            var employees = _unitofWork._EmployeeRepository.GetAllActiveEmployees();
            foreach (var emp in employees)
            {
                var dept = _unitofWork._DepartmentRepository.GetDepartmentById((int) emp.DepartmentId);
                var designation = _unitofWork._DesignationRepository.GetDesignationById((int) emp.DesignationId);
                var empUser = _unitofWork._UserRepository.GetSingleUser(emp.Id);
                var category = Enum.GetName(typeof(EmployeeCategory), emp.EmployeeCategory);
                var type = Enum.GetName(typeof(EmployeeType), emp.EmployeeType);

                empList.Add(new EmployeeList(emp.Id, empUser.FullName, emp.PhoneNumber, empUser.ProfileImageUrl,
                    empUser.Email, emp.Address, emp.EmployeeNumber, emp.DateEmployed, emp.DateofBirth, emp.DateConfirmed,
                    dept.Title, designation.Title, type, category));
            }
            ViewBag.datasource = empList;

            return View(pModel);
        }

       
        public ActionResult Create()
        {
            var roleList = ((ClaimsIdentity) User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var departments = _unitofWork._DepartmentRepository.GetAllDepartments();


            DatePickerProperties datemodel = new DatePickerProperties();
            datemodel.DateFormat = "MM/dd/yyyy";
            ViewData["date"] = datemodel;


            var viewModel = new EmployeeViewModel()
            {
                DepartmentList = departments,
                Menus = menus,
                Heading = "Add new Employee"
            };
            return View(viewModel);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel model)
        {

            ApplicationUser user = null;

            if (!ModelState.IsValid)
            {
                var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


                var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
                var departments = _unitofWork._DepartmentRepository.GetAllDepartments();


                DatePickerProperties datemodel = new DatePickerProperties();
                datemodel.DateFormat = "MM/dd/yyyy";
                ViewData["date"] = datemodel;


                var viewModel = new EmployeeViewModel()
                {
                    DepartmentList = departments,
                    Menus = menus,
                    Heading = "Add new Employee"
                };
                return View("Create",viewModel);
            }

            UserStore userStore = new EmployeeStore();
            user = userStore.createUser(UserType.Employee, model);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DateofBirth = model.DateofBirth;
            user.PhoneNumber = model.Phone;
            user.Gender = model.Gender;
            user.Address = model.Address;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.ProfileImageUrl = "avatar.png";
            model.UserType = UserType.Employee;


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

                    UserManager.AddToRole(user.Id, "Employee");
                    Employee emp = (Employee) user;
                    if (emp.EmployeeCategory == EmployeeCategory.Teaching)
                        UserManager.AddToRole(user.Id, "Teacher");
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Employee");
                }
                AddErrors(result);
            

            // If we got this far, something failed, redisplay form
            return View(model);
        }

       
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [Authorize]
        public ActionResult Edit(string id)
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var departments = _unitofWork._DepartmentRepository.GetAllDepartments();
           

            
            DatePickerProperties datemodel = new DatePickerProperties();
            datemodel.DateFormat = "MM/dd/yyyy";
            ViewData["date"] = datemodel;

            var employee = _unitofWork._EmployeeRepository.GetEmployeeById(id);

            var designations =
                _unitofWork._DesignationRepository.GetAllDesignationsBySchoolDepartment((int) employee.DepartmentId);
            var model = new EmployeeViewModel();
            model.Id = id;
            model.DepartmentId = (int)employee.DepartmentId;
            model.IsConfirmed = employee.IsConfirmed;
            model.DateConfirmed = employee.DateConfirmed;
            model.DateEmployed = employee.DateEmployed;
            model.DesignationId = (int) employee.DesignationId;
            model.EmployeeCategory = employee.EmployeeCategory;
            model.EmployeeType = employee.EmployeeType;
            model.Heading = "Edit Employee";
            model.Menus = menus;
            model.DepartmentList = departments;
            model.DesignationList = designations;
            model.Address = employee.Address;
            model.DateofBirth = employee.DateofBirth;
            model.Gender = employee.Gender;
            model.Email = employee.Email;
            model.FirstName = employee.FirstName;
            model.LastName = employee.LastName;
            model.IsActive = employee.IsActive;
            model.UserType = UserType.Employee;
            model.Phone = employee.PhoneNumber;
            

            return View("Edit", model);

        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EmployeeViewModel model)
        {
            

            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var departments = _unitofWork._DepartmentRepository.GetAllDepartments();
            var emp = _unitofWork._EmployeeRepository.GetEmployeeById(model.Id);
            var designations =
                _unitofWork._DesignationRepository.GetAllDesignationsBySchoolDepartment((int) emp.DepartmentId);

            DatePickerProperties datemodel = new DatePickerProperties();
            datemodel.DateFormat = "MM/dd/yyyy";
            ViewData["date"] = datemodel;

            if (!ModelState.IsValid)
            {
                var viewModel = new EmployeeViewModel()
                {
                    DepartmentList = departments,
                    Menus = menus,
                    DesignationList = designations,
                    Heading = "Add new Employee"
                };
                return View("Edit", viewModel);
            }
            
            var employee = _unitofWork._EmployeeRepository.GetEmployeeById(model.Id);
            employee.DateConfirmed = model.DateConfirmed;
            employee.DateEmployed = model.DateEmployed;
            employee.DepartmentId = model.DepartmentId;
            employee.DesignationId = model.DesignationId;
            employee.EmployeeCategory = model.EmployeeCategory;
            employee.EmployeeType = model.EmployeeType;
            employee.IsConfirmed = model.IsConfirmed;
            employee.Address = model.Address;
            employee.DateofBirth = model.DateofBirth;
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
           if(model.IsActive)
                employee.ReActivate();
           else if (!model.IsActive)
           {
               employee.Deactivate();
           }
            employee.PhoneNumber = model.Phone;
            
            _unitofWork.Complete();
            return RedirectToAction("Index", "Employee");
        }
    }


}