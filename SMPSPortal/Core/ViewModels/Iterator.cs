using System;

namespace SmpsPortal.Core.ViewModels
{
    public interface Iterator
    {
        bool hasNext();
        Object next();
    }
}
