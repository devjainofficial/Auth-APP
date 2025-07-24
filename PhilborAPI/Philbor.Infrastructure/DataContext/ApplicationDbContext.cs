using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Philbor.Domain.Models;

namespace Philbor.Infrastructure.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.Database.SetCommandTimeout(180);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
            SeedUsers(builder);
        }

        public void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int> { Id = 2, Name = "User", NormalizedName = "USER" }
            );
        }

        public void SeedUsers(ModelBuilder builder)
        {
            // Create Admin user
            var adminUserId = 1;
            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                FirstName = "Kishan",
                LastName = "Suthar",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "kishan.suthar@zobiwebsolutions.com",
                NormalizedEmail = "KISHAN.SUTHAR@ZOBIWEBSOLUTIONS.COM",
                EmailConfirmed = true,
                Gender = "Male",
                TwoFactorEnabled = true,
                SecurityStamp = "f1e2b556-6d47-4d45-909c-b1278dc5ff52",
                PasswordHash = hasher.HashPassword(null, "Admin@123")
            };

            builder.Entity<ApplicationUser>().HasData(adminUser);

            // Assign Admin role to admin user
            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = adminUserId
                }
            );
        }

    }
}
