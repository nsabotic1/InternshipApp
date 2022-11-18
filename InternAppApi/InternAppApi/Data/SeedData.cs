using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternAppApi.Data
{
    public class SeedData
    {
        //Seed admin role into database
        public static void SeedAdminRole(ModelBuilder builder){
              builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "e91e639c-27b9-44de-9fd8-efd62be07517", Name = "Admin", NormalizedName = "ADMIN".ToUpper() });
        }

        public static void SeedAdminUser(ModelBuilder builder){
            
            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();

            //Seeding the User to AspNetUsers table
            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "044fd6d7-e011-4d9d-b050-6302884a975d", // primary key
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin1.")
                }
            );
        }

        public static void SeedUserRoleRelationship(ModelBuilder builder){
            //Seeding the relation between our user and role to AspNetUserRoles table
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "e91e639c-27b9-44de-9fd8-efd62be07517",
                    UserId = "044fd6d7-e011-4d9d-b050-6302884a975d"
                }
                );
        }
    }
}