using System;

namespace SmpsPortal.Core.Models
{
    public abstract class MenuComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int MenuOrder { get; set; }
        public string Description { get; set; }

        public string Icon { get; set; }

        public virtual void add(MenuComponent menuComponent) { throw new NotSupportedException("add function not supported at this level!!"); }

        public virtual void remove(MenuComponent menuComponent) { throw new NotSupportedException("remove function not supported at this level!!"); }

        public virtual MenuComponent getChild(int i) { throw new NotSupportedException("getChild function not supported at this level!!"); }

        public virtual int getId() { throw new NotSupportedException("getId function not supported at this level!!"); }

        public virtual string getName() { throw new NotSupportedException("getName function not supported at this level!!"); }

        public virtual string getDescription() { throw new NotSupportedException("getDescription function not supported at this level!!"); }

        public virtual MenuComponent getParent(int i) { throw new NotSupportedException("getParent function not supported at this level!!"); }

        public virtual string getUrl() { throw new NotSupportedException("getUrl function not supported at this level!!"); }

        public virtual int getMenuOrder() { throw new NotSupportedException("getMenuOrder function not supported at this level!!"); }

        public virtual void print() { throw new NotSupportedException("print function not supported at this level!!"); }



    }
}