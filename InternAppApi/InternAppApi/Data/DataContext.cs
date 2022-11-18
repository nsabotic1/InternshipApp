using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternAppApi.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Application>? Applications { get; set; }
        public DbSet<Selection>? Selections { get; set; }
        public DbSet<ApplicationComment> ApplicationComment { get; set; }
        public DbSet<SelectionComment>? SelectionComment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //seeding administrator data into DB
            SeedData.SeedAdminRole(modelBuilder);
            SeedData.SeedAdminUser(modelBuilder);
            SeedData.SeedUserRoleRelationship(modelBuilder);
        }
    }
}