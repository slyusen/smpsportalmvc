using System;

namespace SmpsPortal.Core.ViewModels
{
    [Serializable]
    public class DesignationList
    {

        public DesignationList()
        {

        }

        public DesignationList(int id, string title, string departmentTitle)
        {
            this.Id = id;

            this.Title = title;

            this.DepartmentTitle = departmentTitle;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string DepartmentTitle { get; set; }

    }
}