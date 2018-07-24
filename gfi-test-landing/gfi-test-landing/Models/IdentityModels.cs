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


    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
       // : base("gfipt0581.testLanding.dbo")
        : base("gfipt0583.testLanding.dbo")
        //: base("gfipt0369.testLanding.dbo")
        {
        }

        //protected override void OnModelCreating(DbModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<ApplicationUser>().ToTable("AspUsers");
        //    //modelBuilder.Entity<ApplicationUser>()
        //    //    .ToTable("Users", "dbo").Property(p => p.Id).HasColumnName("User_Id");
        //}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


    }
}