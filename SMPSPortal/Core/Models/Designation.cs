namespace SmpsPortal.Core.Models
{
    public class Designation
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public  int DepartmentId { get; set; }

        public virtual Department Department { get; set; }




    }
}