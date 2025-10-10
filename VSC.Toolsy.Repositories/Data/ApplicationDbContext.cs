using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection") ?? throw new Exception("connectionString is null");

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options => options.EnableRetryOnFailure(5));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ValueConverter rolesConverter = new ValueConverter<List<UserRole>, string>(
         v => JsonConvert.SerializeObject(v),
         v => JsonConvert.DeserializeObject<List<UserRole>>(v)
     );

            ValueComparer rolesComparer = new ValueComparer<List<UserRole>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
            );

            modelBuilder.Entity<Profile>()
                .Property(p => p.Roles)
                .HasConversion(rolesConverter)
                .Metadata
                .SetValueComparer(rolesComparer);

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.RefreshToken)
                .WithOne(rt => rt.profile)
                .HasForeignKey<RefreshToken>(rt => rt.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);


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
                   Roles = new List<UserRole> { UserRole.Admin },
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
                Roles = new List<UserRole> { UserRole.Admin },
                IsActive = true,
                EmailVerifiedAt = DateTime.Now,
                PhoneVerifiedAt = DateTime.Now
            });


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Tool> Tools { get; set; }

        public DbSet<ToolImage> ToolImages { get; set; }

        public DbSet<SigningKey> SigningKeys { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
