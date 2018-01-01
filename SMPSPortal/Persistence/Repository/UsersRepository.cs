using SmpsPortal.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SmpsPortal.Core.Repositories;

namespace SmpsPortal.Persistence.Repository
{
    public class UsersRepository : IUserRepository
    {
        ApplicationDbContext _context;
        public UserManager<ApplicationUser> userManager;
        public RoleManager<IdentityRole> roleManager;
        private string url;
        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;

            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));

           
        }

      
        public IEnumerable<ApplicationUser> GetAppUsers(string roleId)
        {
            return from user in _context.Users
                       where user.Roles.Any(r => r.RoleId == roleId)
                       select user;
           
        }

        

        public IEnumerable<ApplicationUser> GetUsersInGroup(string userGroupName)
        {
            switch (userGroupName)
            {
                case "Parents":
                    return GetUsersInRole("Parent");
                case "Students":
                    return GetUsersInRole("Student");
                case "Teachers":
                    return GetUsersInRole("Teacher");
                case "All Staff":
                    return GetUsersInRole("Employee");
                default:
                    return GetAllUsers();

            }
        }

        public IEnumerable<ApplicationUser> GetUsersInRole(string roleName)
        {
            var role = _context.Roles.SingleOrDefault(r => r.Name == roleName);
            
            return from user in _context.Users
                   where user.Roles.Any(r => r.RoleId == role.Id)
                   select user;
        }
        
        public void AddUserToRole(string username, string roleName)
        {
            Roles.AddUserToRole(username, roleName);

        }
        public void RemoveUserFromRole(string username, string roleName)
        {
            Roles.RemoveUserFromRole(username, roleName);
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _context.Users;
        }

        public IEnumerable<ApplicationUser> GetAllUsersNotInRole(string roleId)
        {
            return from user in _context.Users
                   where user.Roles.Any(r => r.RoleId != roleId)
                   select user;
        }
        public ApplicationUser GetSingleUser(string userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId);
        }
        public ApplicationUser GetSingleUserByName(string name)
        {
            return _context.Users.SingleOrDefault(u => u.FullName == name);
        }
        public MedicalRecord GetMedicalRecordByUserId(string userId)
        {
            return _context.MedicalRecords.SingleOrDefault(m => m.AppUserId == userId);
        }
        public UserManager<ApplicationUser> GetUserManager()
        {

            return userManager;
        }
        public void AddMedicalRecord(MedicalRecord med)
        {
            _context.MedicalRecords.Add(med);
        }

       
        public void RemoveMedicalRecord(MedicalRecord med)
        {
            _context.MedicalRecords.Remove(med);
        }

        public RoleManager<IdentityRole> GetRoleManager()
        {
            return roleManager;
        }
    }
}