using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Models
{
    public class EmployeeStore : UserStore
    {
        public override ApplicationUser createUser(UserType userType, RegisterViewModel model)
        {
            if (userType.Equals(UserType.Employee))
            {
                return new Employee(model);
            }
            else return null;
        }
    }
}