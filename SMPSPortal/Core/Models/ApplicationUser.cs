using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using SmpsPortal.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SmpsPortal.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public abstract class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

       
        [Display(Name = "Date of Bith")]
        public DateTime DateofBirth { get; set; }

        
        [StringLength(500)]
        public string Address { get; set; }


        [StringLength(500)]
        public string ProfileImageUrl { get; set; }

        [StringLength(500)]
        public string AboutMe { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }


        public Gender Gender { get; set; }

        public bool IsActive { get; private set; }

        public ICollection<UserNotification> UserNotifications { get; set; }

       

        public ApplicationUser()
        {
            UserNotifications = new Collection<UserNotification>();
            IsActive = true;
            ProfileImageUrl = "";

        }


        public void Notify(Notification notification)
        {
            UserNotifications.Add(new UserNotification(this, notification));
        }



        public void Deactivate()
        {
            IsActive = false;
        }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string Get8Digits()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return String.Format("{0:D8}", random);
        }

        public void ReActivate()
        {
            IsActive = true;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }


}