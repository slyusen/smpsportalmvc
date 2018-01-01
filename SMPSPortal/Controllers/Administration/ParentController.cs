using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
using Microsoft.AspNet.Identity.Owin;
using Syncfusion.JavaScript.Models;

namespace SmpsPortal.Controllers.Administration
{
    public class ParentController : Controller
    {
        private ApplicationUserManager _userManager;
        public IUnitofWork _unitofWork;
        private List<ParentList> parentList;
        private MenuRepository menuRepo;

        public ParentController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            parentList = new List<ParentList>();
            menuRepo = new MenuRepository();
            
        }
        public ParentController(ApplicationUserManager userManager)
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

        public ActionResult Index()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
               .Where(ck => ck.Type == ClaimTypes.Role)
               .Select(c => c.Value).ToList();
            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var pModel = new ParentViewModel()
            {
                Menus = menus,
                Heading = "Parents"
            };
            var parents = _unitofWork._ParentRepository.GetAllParents();
            foreach (var prt in parents)
            {
                parentList.Add(new ParentList(prt.Id, prt.ProfileImageUrl, prt.FullName, prt.Email, prt.PhoneNumber, prt.Occupation));

            }
            ViewBag.datasource = parentList;

            return View(pModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
               .Where(ck => ck.Type == ClaimTypes.Role)
               .Select(c => c.Value).ToList();
            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var viewModel = new ParentViewModel()
            {
                Menus = menus,
                Heading = "Add new Parent"
            };
            return View("Create", viewModel);
        }

        [Authorize]
        public ActionResult StudentParent(string id)
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();
            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            if (id == null)
                id = TempData["ParentId"].ToString();
            var spList = new List<StudentParentList>();
            var studParents = _unitofWork._StudentParentRepository.GetAllStudentParentsByParent(id);
            foreach (var sp in studParents)
            {
                var student = _unitofWork._StudentRepository.GetStudentById(sp.StudentId);
                
                var parent = _unitofWork._ParentRepository.GetParentById(sp.ParentId);
               

                spList.Add(new StudentParentList(sp.Id, student.FullName, parent.FullName, sp.RelationshipToStudent));
            }
            ViewBag.datasource = spList;
            
            var thisparent = _unitofWork._ParentRepository.GetParentById(id);
           
            var pModel = new ParentViewModel()
            {
                Menus = menus,
                Heading = "Assigned Students for " + thisparent.FullName,
                Id = id

            };
            return View(pModel);
        }

        [Authorize]
        public ActionResult AssignStudent(string id)
        {
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "Father", Value = "Father" });
            listItem.Add(new SelectListItem { Text = "Mother", Value = "Mother" });
            listItem.Add(new SelectListItem { Text = "Uncle", Value = "Uncle" });
            listItem.Add(new SelectListItem { Text = "Aunty", Value = "Aunty" });
            listItem.Add(new SelectListItem { Text = "Brother", Value = "Brother" });
            listItem.Add(new SelectListItem { Text = "Sister", Value = "Sister" });
            listItem.Add(new SelectListItem { Text = "Guardian", Value = "Guardian" });

            var gradeLevel = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();

            var roleList = ((ClaimsIdentity)User.Identity).Claims
               .Where(ck => ck.Type == ClaimTypes.Role)
               .Select(c => c.Value).ToList();
            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var thisParent = _unitofWork._ParentRepository.GetParentById(id);
            var pModel = new ParentViewModel()
            {
                Menus = menus,
                Heading = "Assign Students to " + thisParent.FullName,
                RelationshipList = listItem,
                GradeLevelList = gradeLevel,
                Id = id

            };

            return View("StudentParentForm", pModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignStudent(ParentViewModel viewModel)
        {
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "Father", Value = "Father" });
            listItem.Add(new SelectListItem { Text = "Mother", Value = "Mother" });
            listItem.Add(new SelectListItem { Text = "Uncle", Value = "Uncle" });
            listItem.Add(new SelectListItem { Text = "Aunty", Value = "Aunty" });
            listItem.Add(new SelectListItem { Text = "Brother", Value = "Brother" });
            listItem.Add(new SelectListItem { Text = "Sister", Value = "Sister" });
            listItem.Add(new SelectListItem { Text = "Guardian", Value = "Guardian" });
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();
            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var thisParent = _unitofWork._ParentRepository.GetParentById(viewModel.Id);

            var gradeLevel = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();
            if (viewModel.StudentId == null)
            {
                viewModel.Menus = menus;
                viewModel.Heading = "Assign Students to " + thisParent.FullName;
                viewModel.GradeLevelList = gradeLevel;
                viewModel.RelationshipList = listItem;
                viewModel.Message = "Choose a Student";
                return View("StudentParentForm", viewModel);
            }
            var studP = _unitofWork._StudentParentRepository.GetStudentParentByStudentParentId(viewModel.StudentId,
                viewModel.Id);

            if (studP != null)
            {
                viewModel.Menus = menus;
                viewModel.Heading = "Assign Students to " + thisParent.FullName;
                viewModel.GradeLevelList = gradeLevel;
                viewModel.RelationshipList = listItem;
                viewModel.Message = "Student already assigned to this Parent";
                return View("StudentParentForm", viewModel);
            }
            else
            {
              
                var studParent = new StudentParent()
                {
                    StudentId = viewModel.StudentId,
                    ParentId = viewModel.Id,
                    RelationshipToStudent = viewModel.Relationship,
                    
                };

                _unitofWork._StudentParentRepository.Add(studParent);
                _unitofWork.Complete();
            }
            TempData["ParentId"] = viewModel.Id;
            return RedirectToAction("StudentParent", "Parent");
        }

        [Authorize]
        public ActionResult Edit(string id)
        {

            
            var parent = _unitofWork._ParentRepository.GetParentById(id);
            var roleList = ((ClaimsIdentity)User.Identity).Claims
              .Where(ck => ck.Type == ClaimTypes.Role)
              .Select(c => c.Value).ToList();
            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            
            DatePickerProperties datemodel = new DatePickerProperties();
            datemodel.DateFormat = "MM/dd/yyyy";
            ViewData["date"] = datemodel;
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "Male", Value = "Male" });
            listItem.Add(new SelectListItem { Text = "Female", Value = "Female" });
            var viewModel = new ParentViewModel()
            {
                GenderList = listItem,
                Gender = parent.Gender,
                FirstName = parent.FirstName,
                LastName = parent.LastName,
                Phone = parent.PhoneNumber,
                Email = parent.Email,
                Occupation = parent.Occupation,
                Address = parent.Address,
                BusinessAddress = parent.BusinessAddress,
                DateofBirth = parent.DateofBirth,
                IsActive =  parent.IsActive,
                Menus = menus,
                Heading = "Edit Parent",
                Id = parent.Id

            };
            return View("Edit", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParentViewModel model)
        {

            var roleList = ((ClaimsIdentity)User.Identity).Claims
             .Where(ck => ck.Type == ClaimTypes.Role)
             .Select(c => c.Value).ToList();
            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
           
            DatePickerProperties datemodel = new DatePickerProperties();
            datemodel.DateFormat = "MM/dd/yyyy";
            ViewData["date"] = datemodel;
           
            if (!ModelState.IsValid)
            {
                
                model.Menus = menus;
                return View("Create", model);
            }

            ApplicationUser user = null;



            UserStore userStore = new ParentStore();
            user = userStore.createUser(UserType.Parent, model);
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
                UserManager.AddToRole(user.Id, "Parent");

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return RedirectToAction("Index", "Parent");
            }

            


            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ParentViewModel model)
        {

            var roleList = ((ClaimsIdentity)User.Identity).Claims
                 .Where(ck => ck.Type == ClaimTypes.Role)
                 .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var gradelist = _unitofWork._GradeLevelRepository.GetAllGradeLevelBySchool();

            DatePickerProperties datemodel = new DatePickerProperties();
            datemodel.DateFormat = "MM/dd/yyyy";
            ViewData["date"] = datemodel;

            var parent = _unitofWork._ParentRepository.GetParentById(model.Id);

           

            if (!ModelState.IsValid)
            {
                var viewModel = new ParentViewModel()
                {
                    
                    Menus = menus,
                    GradeLevelList = gradelist,
                    Heading = "Add Parent"
                };
                return View("Edit", viewModel);
            }



            parent.BusinessAddress = model.BusinessAddress;
            parent.Occupation = model.Occupation;
            parent.Address = model.Address;
            parent.DateofBirth = model.DateofBirth;
            parent.FirstName = model.FirstName;
            parent.LastName = model.LastName;
            if (model.IsActive)
                parent.ReActivate();
            else if (!model.IsActive)
            {
                parent.Deactivate();
            }
            parent.PhoneNumber = model.Phone;

            _unitofWork.Complete();
            return RedirectToAction("Index", "Parent");
        }
    }
}
