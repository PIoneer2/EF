using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EF.Core;
using EF.Core.Data;

namespace EF.Core.Data
{
    public class User : IdentityUser<long, UserLogin, UserRole, UserClaim>, IBaseEntity
    {
        public User()
        {
            this.Transactions = new HashSet<Transactions>();
            this.Roles = new HashSet<UserRole>();
            this.Logins = new HashSet<UserLogin>();
            this.Claims = new HashSet<UserClaim>();
        }

        //
        // Сводка:
        //     Used to record failures for the purposes of lockout
        public override int AccessFailedCount { get; set; }
        //
        // Сводка:
        //     Email
        public override string Email { get; set; }
        //
        // Сводка:
        //     True if the email is confirmed, default is false
        public override bool EmailConfirmed { get; set; }
        //
        // Сводка:
        //     Is lockout enabled for this user
        public override bool LockoutEnabled { get; set; }
        //
        // Сводка:
        //     DateTime in UTC when lockout ends, any time in the past is considered not locked
        //     out.
        public override DateTime? LockoutEndDateUtc { get; set; }
        //
        // Сводка:
        //     The salted/hashed form of the user password
        public override string PasswordHash { get; set; }
        //
        // Сводка:
        //     PhoneNumber for the user
        public override string PhoneNumber { get; set; }
        //
        // Сводка:
        //     True if the phone number is confirmed, default is false
        public override bool PhoneNumberConfirmed { get; set; }
        //
        // Сводка:
        //     A random value that should change whenever a users credentials have changed (password
        //     changed, login removed)
        public override string SecurityStamp { get; set; }
        //
        // Сводка:
        //     Is two factor enabled for the user
        public override bool TwoFactorEnabled { get; set; }
        //
        // Сводка:
        //     User name
        public override string UserName { get; set; }

        public override long Id { get; set; }
    
        public virtual ICollection<Transactions> Transactions { get; set; }
        //
        // Сводка:
        //     Navigation property for user roles
        public override ICollection<UserRole> Roles { get; }
        //
        // Сводка:
        //     Navigation property for user logins
        public override ICollection<UserLogin> Logins { get; }
        //
        // Сводка:
        //     Navigation property for user claims
        public override ICollection<UserClaim> Claims { get; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, long> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
