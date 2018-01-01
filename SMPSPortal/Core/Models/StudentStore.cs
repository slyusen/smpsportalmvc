using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Models
{
    public class StudentStore : UserStore
    {
        public override ApplicationUser createUser(UserType userType, RegisterViewModel model)
        {
            if (userType.Equals(UserType.Student))
            {
                return new Student(model);
            }
            else return null;
        }
    }
}