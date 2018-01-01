namespace SmpsPortal.Core.Models
{
    public class StudentParent
    {
        public int Id { get; set; }

        public  string StudentId { get; set; }
        public virtual Student Student { get; set; }

        public  string ParentId { get; set; }
        public virtual Parent Parent { get; set; }

        public string RelationshipToStudent { get; set; }

    }
}