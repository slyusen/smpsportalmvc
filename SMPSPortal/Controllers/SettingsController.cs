using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmpsPortal.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SmpsPortal.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        [Authorize]
        public ActionResult SchoolRoles()
        {
            return View();
        }
    }
}