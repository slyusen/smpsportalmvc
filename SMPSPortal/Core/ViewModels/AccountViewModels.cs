using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public UserType userType { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }


    }

    public abstract class RegisterViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "First Name too Long")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Last Name too Long")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(13)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        
        [Display(Name = "Date of Bith")]
        public DateTime DateofBirth { get; set; }

       

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address!!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> GenderList { get; set; }

        
        public Gender Gender { get; set; }

       
       
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; }


        //Employee specific
        public virtual DateTime DateEmployed { get; set; }

        public virtual EmployeeCategory EmployeeCategory { get; set; }

        public virtual EmployeeType EmployeeType { get; set; }

        public virtual int DepartmentId { get; set; }

        public virtual int DesignationId { get; set; }

        public virtual bool IsConfirmed { get; set; }

        public virtual DateTime DateConfirmed { get; set; }

        //Student specific
        public virtual  DateTime DateEnrolled { get; set; }
        public  virtual int SchoolClassId { get; set; }
        public virtual  int GradeLevelId { get; set; }

        //Parent specific
        public virtual string Occupation { get; set; }
        public virtual  string BusinessAddress { get; set; }

        protected RegisterViewModel()
        {
                DateEnrolled = DateTime.Today;
                DateConfirmed = DateTime.Today;
                DateEmployed = DateTime.Today;
                DateofBirth = DateTime.Today;
        }
}

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
