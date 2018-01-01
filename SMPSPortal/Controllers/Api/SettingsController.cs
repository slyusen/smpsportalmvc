using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using SmpsPortal.Core;
using SmpsPortal.Core.Dtos;
using SmpsPortal.Persistence;

namespace SmpsPortal.Controllers.Api
{
    public class SettingsController : ApiController
    {
        private IUnitofWork _unitofWork;
        public SettingsController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
        }

        [HttpPost]
        public IHttpActionResult Index(UserRolesDto dto)
        {

            var roleName = dto.ValuesList[1];

            var roleId = dto.ValuesList[0];


            var currentUsers = _unitofWork._UserRepository.GetUsersInRole(roleName);

            var userMgr = _unitofWork._UserRepository.GetUserManager();



            _unitofWork.Complete();

            foreach (var user in currentUsers)
            {
                var result = userMgr.RemoveFromRoleAsync(user.Id, roleName);
                _unitofWork.Complete();
            }
            for (int i = 2; i < dto.ValuesList.Count; i++)
            {
                var currentUser = _unitofWork._UserRepository.GetSingleUserByName(dto.ValuesList[i]);
                var res = userMgr.AddToRoleAsync(currentUser.Id, roleId);
                _unitofWork.Complete();
            }

            HttpContext.Current.Response.Redirect(Request.RequestUri.PathAndQuery);
            return Ok();
        }
    }
}
