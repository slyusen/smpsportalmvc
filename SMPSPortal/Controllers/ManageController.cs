using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SmpsPortal.Core;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.ViewModels;
using SmpsPortal.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using SmpsPortal.Persistence.Repository;

namespace SmpsPortal.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public IUnitofWork _unitofWork;
        private MenuRepository menuRepo;

        public ManageController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            menuRepo = new MenuRepository();
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
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

        //
        // GET: /Manage/Index
        public ActionResult SaveDefault(IEnumerable<HttpPostedFileBase> UploadDefault)

        {
            var currentUserId = User.Identity.GetUserId();
            var cUser = _unitofWork._UserRepository.GetSingleUser(currentUserId);

            foreach (var file in UploadDefault)

            {
                var currentfileName = cUser.ProfileImageUrl;
                if (currentfileName != "avatar.png")
                {
                    var physicalPath = Path.Combine(Server.MapPath("~/Images/Profile"), currentfileName);

                    if (System.IO.File.Exists(physicalPath))

                    {

                        System.IO.File.Delete(physicalPath);

                    }
                }

                var ext = Path.GetExtension(file.FileName);
                var fileName = "IMG" + currentUserId.Substring(0, 4) + Get6Digits() + ext;
                cUser.ProfileImageUrl = fileName;
                _unitofWork.Complete();

                var destinationPath = Path.Combine(Server.MapPath("~/Images/Profile"), fileName);

                file.SaveAs(destinationPath);
                ViewBag.StatusMessage = "Image Uploaded";
                return Content("Image Uploaded");

            }

            return Content("");

        }
        public string Get6Digits()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            var random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return String.Format("{0:D6}", random);
        }
        public ActionResult RemoveDefault(string[] fileNames)
        {

            var currentUserId = User.Identity.GetUserId();
            var cUser = _unitofWork._UserRepository.GetSingleUser(currentUserId);
            foreach (var fullName in fileNames)
            {
                var fileName = cUser.ProfileImageUrl;
                var physicalPath = Path.Combine(Server.MapPath("~/Images/Profile"), fileName);

                if (System.IO.File.Exists(physicalPath))

                {

                    System.IO.File.Delete(physicalPath);

                }

            }

            return Content("");

        }
        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : message == ManageMessageId.ProfileEditSuccess ? "Profile Edited!!."
                : "";

            var userId = User.Identity.GetUserId();
            var cUser = _unitofWork._UserRepository.GetSingleUser(userId);
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                    .Where(ck => ck.Type == ClaimTypes.Role)
                    .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var model = new IndexViewModel
            {
                ProfileImageUrl = cUser.ProfileImageUrl,
                Heading = "Edit Profile",
                Menus = menus,
                FirstName = cUser.FirstName,
                LastName = cUser.LastName,
                AboutMe = cUser.AboutMe,
                Email = cUser.Email,
                UserName = cUser.UserName,
                State = cUser.State,
                City = cUser.City,
                Country = cUser.Country,
                DateofBirth = cUser.DateofBirth,
                Address = cUser.Address,
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                   .Where(ck => ck.Type == ClaimTypes.Role)
                   .Select(c => c.Value).ToList(); ;
            var mainmenu = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var vModel = new ChangePasswordViewModel()
            {
                Heading = "Change Password",
                Menus = mainmenu,
            };
            return View(vModel);
        }

        //
        // POST: /Manage/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Index(IndexViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var cUser = _unitofWork._UserRepository.GetSingleUser(userId);

           
            cUser.FirstName = model.FirstName;
            cUser.LastName = model.LastName;
            cUser.Address = model.Address;
            cUser.AboutMe = model.AboutMe;
            cUser.UserName = model.UserName;
            cUser.State = model.State;
            cUser.City = model.City;
            cUser.Country = model.Country;
            cUser.DateofBirth = model.DateofBirth;
            cUser.PhoneNumber = model.PhoneNumber;
          
            _unitofWork.Complete();
            model.ProfileImageUrl = cUser.ProfileImageUrl;
            model.HasPassword = HasPassword();
            ViewBag.StatusMessage = "Profile Updated!!";
            return View(model);

        }


        public ActionResult ChangeUserPassword(string id)
        {
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                   .Where(ck => ck.Type == ClaimTypes.Role)
                   .Select(c => c.Value).ToList(); ;
            var mainmenu = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var vModel = new ChangeUserPasswordViewModel()
            {
                Heading = "Change User Password",
                Menus = mainmenu,
                UserId = id

            };


            return View(vModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeUserPassword(ChangeUserPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var roleList = ((ClaimsIdentity)User.Identity).Claims
                  .Where(ck => ck.Type == ClaimTypes.Role)
                  .Select(c => c.Value).ToList(); ;
                var mainmenu = menuRepo.GetCapableMenusWithMenuItems(roleList);
                model.Heading = "Change Password";
                model.Menus = mainmenu;

                return View(model);
            }
            var userPassResetToken = await UserManager.GeneratePasswordResetTokenAsync(model.UserId);
            var result = await UserManager.ResetPasswordAsync(model.UserId, userPassResetToken, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }



        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var roleList = ((ClaimsIdentity)User.Identity).Claims
                  .Where(ck => ck.Type == ClaimTypes.Role)
                  .Select(c => c.Value).ToList(); ;
                var mainmenu = menuRepo.GetCapableMenusWithMenuItems(roleList);
                model.Heading = "Change Password";
                model.Menus = mainmenu;
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            ProfileEditSuccess,
            Error
        }

#endregion
    }
}