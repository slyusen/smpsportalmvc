namespace SmpsPortal.Core.Models
{
    public class MenuItem : MenuComponent
    {

       

        public virtual Menu Menu { get; set; }

        

        public MenuItem()
        {
                
        }

        public MenuItem(string name, string url, int menuOrder, string desc)
        {
            Name = name;
            Url = url;
            MenuOrder = menuOrder;
            Description = desc;
            
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