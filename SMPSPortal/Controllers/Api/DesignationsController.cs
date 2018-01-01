using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using SmpsPortal.Core;
using SmpsPortal.Persistence;

namespace SmpsPortal.Controllers.Api
{
    public class DesignationsController : ApiController
    {
        private IUnitofWork _unitofWork;
        public DesignationsController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult GetDesignations(string id)
        {
            List<SelectListItem> designations = new List<SelectListItem>();
            int deptId = Convert.ToInt32(id);
            var designationList = _unitofWork._DesignationRepository.GetAllDesignationsBySchoolDepartment(deptId);
            designations.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var dsg in designationList)
            {
                designations.Add(new SelectListItem { Text = dsg.Title, Value = dsg.Id.ToString() });
            }


            return Ok(new SelectList(designations, "Value", "Text"));
        }
    }
}
