using Microsoft.EntityFrameworkCore;
using VSC.Toolsy.Common.DTOs.Requests;
using VSC.Toolsy.Common.Enums;
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

            // Seeding two admin profiles
            modelBuilder.Entity<Profile>().HasData(
               new Profile
               {
                   Id = Guid.Parse("a0b5d923-fd53-4a68-913b-7a6db1061e4d"),
                   FirstName = "Admin1",
                   LastName = "Admin1",
                   Email = "admin1@example.com",
                   PhoneNumber = "1234567890",
                   DateOfBirth = new DateTime(1985, 5, 1),
                   Gender = Gender.Male,
                   PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin1password"),
                   ProfileImageUrl = "https://chatgpt.com/c/68d61034-1b68-8327-95e8-27a53e3f858cadmin1",
                   Status = AccountStatus.Active,
                   VerificationStatus = VerificationStatus.Verified,
                   Role = Role.Admin,
                   IsActive = true,
                   EmailVerifiedAt = DateTime.Now,
                   PhoneVerifiedAt = DateTime.Now
               },
            new Profile
            {
                Id = Guid.Parse("b1c1e599-59c1-4b3d-b707-5aab9d3f38db"),
                FirstName = "Admin2",
                LastName = "Admin2",
                Email = "admin2@example.com",
                PhoneNumber = "0987654321",
                DateOfBirth = new DateTime(1986, 7, 15),
                Gender = Gender.Female,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin1password"), // Use a real hash here
                ProfileImageUrl = "https://chatgpt.com/c/68d61034-1b68-8327-95e8-27a53e3f858cadmin2",
                Status = AccountStatus.Active,
                VerificationStatus = VerificationStatus.Verified,
                Role = Role.Admin,
                IsActive = true,
                EmailVerifiedAt = DateTime.Now,
                PhoneVerifiedAt = DateTime.Now
            });
        }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Address> Address { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Tool> Tools { get; set; }

        public DbSet<ToolImage> ToolImages { get; set; }

        public DbSet<SigningKey> SigningKeys { get; set; }
    }
}
