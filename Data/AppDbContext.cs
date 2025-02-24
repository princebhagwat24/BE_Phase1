using BE_Phase1.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_Phase1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key configuration for RolePermission
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            // Seed data for Permissions
            modelBuilder.Entity<Permission>().HasData(
                new Permission { PermissionId = 1, Name = "Commission", Active = true },
                new Permission { PermissionId = 2, Name = "Endorsements", Active = true },
                new Permission { PermissionId = 3, Name = "Risk Management", Active = true },
                new Permission { PermissionId = 4, Name = "Administration", Active = true },
                new Permission { PermissionId = 5, Name = "Utilities", Active = true },
                new Permission { PermissionId = 6, Name = "Inactive", Active = false }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
