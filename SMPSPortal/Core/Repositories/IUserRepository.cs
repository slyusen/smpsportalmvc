using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SmpsPortal.Core.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetAppUsers(string roleId);
        IEnumerable<ApplicationUser> GetUsersInGroup(string userGroupName);
        IEnumerable<ApplicationUser> GetUsersInRole(string roleName);
        void AddUserToRole(string username, string roleName);
        void RemoveUserFromRole(string username, string roleName);
        IEnumerable<ApplicationUser> GetAllUsers();
        IEnumerable<ApplicationUser> GetAllUsersNotInRole(string roleId);
        ApplicationUser GetSingleUser(string userId);
        UserManager<ApplicationUser> GetUserManager();
        RoleManager<IdentityRole> GetRoleManager();
        ApplicationUser GetSingleUserByName(string name);

        MedicalRecord GetMedicalRecordByUserId(string userId);

        void AddMedicalRecord(MedicalRecord med);

        void RemoveMedicalRecord(MedicalRecord med);
    }
}
