using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "479e4a3e-ffc7-4564-ad75-f43352dfa0ca";
            var writerRoleId = "74c9e8bd-c2fa-4047-ac24-cee993457341";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName="Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName="Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            // Create an Admin user
            var adminUserId = "9a9a1d68-aaac-4dd4-81a4-dbe0d4a89051";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName="admin@rezadev.com", 
                Email = "admin@rezadev.com",
                NormalizedEmail = "admin@rezadev.com".ToUpper(),
                NormalizedUserName = "admin@rezadev.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "password");
            builder.Entity<IdentityUser>().HasData(admin);

            // Give roles to admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId,
                },
                new IdentityUserRole<string>()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId,
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);


        }
    }
}
