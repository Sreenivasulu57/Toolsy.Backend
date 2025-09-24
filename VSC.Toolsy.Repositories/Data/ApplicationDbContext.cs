using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.Models.CoreEntites;


namespace VSC.Toolsy.Repositories.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Address> Address { get; set; }

        public DbSet<Owner> Owners { get; set; }
    }
}
