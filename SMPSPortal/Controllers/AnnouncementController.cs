using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using SmpsPortal.Core;
using SmpsPortal.Core.Models;
using Microsoft.AspNet.Identity;
using Syncfusion.JavaScript.Models;
using SmpsPortal.Core.ViewModels;
using SmpsPortal.Persistence;
using SmpsPortal.Persistence.Repository;

namespace SmpsPortal.Controllers
{
    public class AnnouncementController : Controller
    {
        private IUnitofWork _unitofWork;
        private MenuRepository menuRepo;

        public AnnouncementController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            menuRepo = new MenuRepository();
        }

        [Authorize]
        public ActionResult Index()
        {
           var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var announcements = _unitofWork._AnnouncementRepository.GetMyAnnouncements(userId);

            var aModel = new AnnouncementViewModel()
            {
                Menus = menus,
                Heading = "My Announcements",
                Announcements = announcements,
                IsEditable = true
            };

            return View(aModel);
        }

        [Authorize]
        public ActionResult Current()
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);

            var announcements = _unitofWork._AnnouncementRepository.GetAnnouncementsUserisInvolvedWith(userId);

            var aModel = new AnnouncementViewModel()
            {
                Menus = menus,
                Heading = "Current Announcements",
                Announcements = announcements,
                IsEditable = false
            };

            return View("Index", aModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            
           
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "All Users", Value = "All Users" });
            listItem.Add(new SelectListItem { Text = "Parents", Value = "Parents" });
            listItem.Add(new SelectListItem { Text = "Students", Value = "Students" });
            listItem.Add(new SelectListItem { Text = "Teachers", Value = "Teachers" });
            listItem.Add(new SelectListItem { Text = "All Staff", Value = "All Staff" });
            DatePickerProperties datemodel = new DatePickerProperties();
            datemodel.DateFormat = "MM/dd/yyyy";
            ViewData["date"] = datemodel;

            var aModel = new AnnouncementViewModel()
            {
                Menus = menus,
                Heading = "Add Announcement",
                UserGroupNames = listItem

            };
            return View("AnnouncementForm", aModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnnouncementViewModel aModel)
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "All Users", Value = "All Users" });
            listItem.Add(new SelectListItem { Text = "Parents", Value = "Parents" });
            listItem.Add(new SelectListItem { Text = "Students", Value = "Students" });
            listItem.Add(new SelectListItem { Text = "Teachers", Value = "Teachers" });
            listItem.Add(new SelectListItem { Text = "All Staff", Value = "All Staff" });
            DatePickerProperties datemodel = new DatePickerProperties();
            datemodel.DateFormat = "MM/dd/yyyy";
            ViewData["date"] = datemodel;

            if (!ModelState.IsValid)
            {
                aModel.Menus = menus;
                aModel.UserGroupNames = listItem;
                return View("AnnouncementForm", aModel);
            }

          

            var ann = new Announcement()
            {
                Title = aModel.Title,
                Message = aModel.Message,
                DateGiven = DateTime.Today,
                DateExpired = aModel.DateExpiry,
                UserGroupName = aModel.UserGroupName,
                AnnouncerId = userId,
                IsActive = aModel.IsActive
                
            };

            _unitofWork._AnnouncementRepository.Add(ann);
            _unitofWork.Complete();

            var usrGrp = _unitofWork._UserRepository.GetUsersInGroup(aModel.UserGroupName).ToList();
            //var client = new TwilioRestClient(Environment.GetEnvironmentVariable("AC9ae059d5e62348d7ba71f5c3d1e8d7c0"), Environment.GetEnvironmentVariable("c64a49eea8d2a593bfdcddc11524644a"));
            foreach (var usr in usrGrp)
            {
                //client.SendMessage("+18562882944", usr.PhoneNumber, aModel.Message);
                var attending = _unitofWork._AttendanceRepository.IsUserAnnouncement(usr.Id, ann.Id);
                if (!attending)
                {
                    var attend = new Attendance()
                    {
                        AttendeeId = usr.Id,
                        AnnouncementId = ann.Id
                    };
                    _unitofWork._AttendanceRepository.Add(attend);
                    _unitofWork.Complete();
                }

            }
            ann.Attendances = _unitofWork._AttendanceRepository.GetAllAttendance().Where(a => a.AnnouncementId == ann.Id).ToList();
            ann.Create();
            _unitofWork.Complete();
            return RedirectToAction("Index", "Announcement");
        }
       

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "All Users", Value = "All Users" });
            listItem.Add(new SelectListItem { Text = "Parents", Value = "Parents" });
            listItem.Add(new SelectListItem { Text = "Students", Value = "Students" });
            listItem.Add(new SelectListItem { Text = "Teachers", Value = "Teachers" });
            listItem.Add(new SelectListItem { Text = "All Staff", Value = "Employees" });
            DatePickerProperties datemodel = new DatePickerProperties();
            datemodel.DateFormat = "MM/dd/yyyy";
            ViewData["date"] = datemodel;

            var announce = _unitofWork._AnnouncementRepository.GetAnnouncementById(id);

            var aModel = new AnnouncementViewModel()
            {
                Menus = menus,
                Heading = "Edit Announcement",
                UserGroupNames = listItem,
                Title = announce.Title,
                UserGroupName = announce.UserGroupName,
                Message = announce.Message,
                DateExpiry = announce.DateExpired,
                IsActive = announce.IsActive,
                Id = announce.Id
            };
            return View("AnnouncementForm", aModel);
        }

        [Authorize]
        public ActionResult Update(AnnouncementViewModel aModel)
        {
           
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text = "All Users", Value = "All Users" });
            listItem.Add(new SelectListItem { Text = "Parents", Value = "Parents" });
            listItem.Add(new SelectListItem { Text = "Students", Value = "Students" });
            listItem.Add(new SelectListItem { Text = "Teachers", Value = "Teachers" });
            listItem.Add(new SelectListItem { Text = "All Staff", Value = "All Staff" });
            DatePickerProperties datemodel = new DatePickerProperties();
            datemodel.DateFormat = "MM/dd/yyyy";
            ViewData["date"] = datemodel;

            if (!ModelState.IsValid)
            {
                aModel.Menus = menus;
                aModel.UserGroupNames = listItem;
                return View("AnnouncementForm", aModel);
            }

            var ann = _unitofWork._AnnouncementRepository.GetActiveAnnoucementswithUserGroup().Single(a => a.Id == aModel.Id);
            var userGroup = _unitofWork._AttendanceRepository.GetAllAttendance().Where(a => a.AnnouncementId == aModel.Id).ToList();
            ann.Modify(aModel.DateExpiry, aModel.IsActive, aModel.Message, aModel.Title, userGroup);
            _unitofWork.Complete();
            return RedirectToAction("Index", "Announcement");
        }
    }
}