using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EF.Core;
using EF.Core.Data;
using EF.Data;
using System;
using System.Collections.Generic;
using EF.Web.Models;

namespace EF.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : AspNetUsers
    {
        public ApplicationUser() 
        {

        }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, long> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, long,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, long, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationDbContext : EFDbContext
    //EFDbContext
    {
        //public new DbSet<ApplicationUser> Users { get; set; }
        /*
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<CustomRole> CustomRoles { get; set; }
        public DbSet<CustomUserRole> CustomUserRoles { get; set; }
        public DbSet<CustomUserClaim> CustomUserClaims { get; set; }
        public DbSet<CustomUserLogin> CustomUserLogins { get; set; }
        */
        /*
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        } */

        public ApplicationDbContext()
            : base()
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}