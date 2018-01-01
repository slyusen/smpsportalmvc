using SmpsPortal.Core.ViewModels;

namespace SmpsPortal.Core.Models
{
    public class MenuCapability
    {
        public int Id { get; set; }

        public int? MenuId { get; set; }
        public Menu Menu { get; set; }

        public int? MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        

        public string RoleName { get; set; }

        public ActionType AllowedAction { get; set; }
    }
}