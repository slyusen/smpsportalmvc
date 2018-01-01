using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Models
{
    public abstract class UserStore
    {
        public abstract ApplicationUser createUser(UserType userType, RegisterViewModel model);
    }
}