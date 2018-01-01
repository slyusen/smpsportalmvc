using System.Data.Entity.Infrastructure;
using SmpsPortal.Persistence;

namespace SmpsPortal.Core.Models
{
    public class MigrationsContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create()
        {
            return ApplicationDbContext.Create();
        }
    }
}