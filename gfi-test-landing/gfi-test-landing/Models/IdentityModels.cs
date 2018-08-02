using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace gfi_test_landing.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
       


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

       

        public string FirstName { get; set; }
        public string LastName { get; set; }


    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        //:base("testLanding")
        //: base("gfipt0581.testLanding.dbo")
        : base("gfipt0583.testLanding.dbo")
        //: base("gfipt0369.testLanding.dbo")
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Renaming default ASP.NET tables
        //    modelBuilder.Entity<IdentityUser>().ToTable("Users");
           
        //    modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
        //    modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
        //    modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
        //    modelBuilder.Entity<IdentityRole>().ToTable("Roles");



        //}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


    }
}