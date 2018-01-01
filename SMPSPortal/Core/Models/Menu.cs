using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SmpsPortal.Core.Models
{
    public class Menu : MenuComponent
    {
        public ArrayList menuComponents = new ArrayList();

        public virtual ICollection<MenuItem> MenuItems { get; private set; }
        public Menu(string name, string description, string url, int menuOrder, string icon)
        {
            this.Name = name;
            this.Description = description;
            this.Url = url;
            this.MenuOrder = menuOrder;
            this.Icon = icon;
            MenuItems = new Collection<MenuItem>();
        }
        public Menu()
        {

        }

        public override void add(MenuComponent menuComponent)
        {
            menuComponents.Add(menuComponent);
        }

        public override void remove(MenuComponent menuComponent)
        {
            menuComponents.Remove(menuComponent);
        }

        public override MenuComponent getChild(int i)
        {
            return (MenuComponent)menuComponents[i];
        }

        public override MenuComponent getParent(int i)
        {
            return (MenuComponent)menuComponents[i];
        }

        public override string getName()
        {
            return Name;
        }

        public override int getId()
        {
            return Id;
        }

        public override string getDescription()
        {
            return Description;
        }

        public override string getUrl()
        {
            return Url;
        }

        public override int getMenuOrder()
        {
            return MenuOrder;
        }
    }
}