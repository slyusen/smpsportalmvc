using System.Collections.Generic;
using System.Collections.ObjectModel;
using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Models
{
    public class Parent : ApplicationUser
    {
        public string Occupation { get; set; }

        public string BusinessAddress { get; set; }

        //public string Phone { get; set; }

        public virtual ICollection<StudentParent> Students { get; private set; }

        public Parent()
        {
            Students = new Collection<StudentParent>();


        }

        public Parent(RegisterViewModel model)
        {
            Occupation = model.Occupation;

            BusinessAddress = model.BusinessAddress;
        }
    }
}