using JobPortal.Model.Model;
using Jobportel.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Jobportel.Data
{
    public class Context: DbContext
    {
        public Context(DbContextOptions options ):base(options)
        {
            
        }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public  DbSet<Job> Job { get; set; }
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<Otp> otp { get; set; }
    }
}
