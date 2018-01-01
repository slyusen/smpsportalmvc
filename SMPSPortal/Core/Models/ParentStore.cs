using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Models
{
    public class ParentStore : UserStore
    {

        public override ApplicationUser createUser(UserType userType, RegisterViewModel model)
        {
            if (userType.Equals(UserType.Parent))
            {
                return new Parent(model);
            }
            else return null;
        }
    }
}