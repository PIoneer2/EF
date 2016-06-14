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
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        [JsonIgnore]
        public override int AccessFailedCount { get; set; }
        //
        // Сводка:
        //     Email
        public override string Email { get; set; }
        //
        // Сводка:
        //     True if the email is confirmed, default is false
        [JsonIgnore]
        public override bool EmailConfirmed { get; set; }
        //
        // Сводка:
        //     Is lockout enabled for this user
        [JsonIgnore]
        public override bool LockoutEnabled { get; set; }
        //
        // Сводка:
        //     DateTime in UTC when lockout ends, any time in the past is considered not locked
        //     out.
        [JsonIgnore]
        public override DateTime? LockoutEndDateUtc { get; set; }
        //
        // Сводка:
        //     The salted/hashed form of the user password
        [JsonIgnore]
        public override string PasswordHash { get; set; }
        //
        // Сводка:
        //     PhoneNumber for the user
        [JsonIgnore]
        public override string PhoneNumber { get; set; }
        //
        // Сводка:
        //     True if the phone number is confirmed, default is false
        [JsonIgnore]
        public override bool PhoneNumberConfirmed { get; set; }
        //
        // Сводка:
        //     A random value that should change whenever a users credentials have changed (password
        //     changed, login removed)
        [JsonIgnore]
        public override string SecurityStamp { get; set; }
        //
        // Сводка:
        //     Is two factor enabled for the user
        [JsonIgnore]
        public override bool TwoFactorEnabled { get; set; }
        //
        // Сводка:
        //     User name
        public override string UserName { get; set; }

        public override long Id { get; set; }

        [JsonIgnore]
        public virtual ICollection<Transactions> Transactions { get; set; }
        //
        // Сводка:
        //     Navigation property for user roles
        [JsonIgnore]
        public override ICollection<UserRole> Roles { get; }
        //
        // Сводка:
        //     Navigation property for user logins
        [JsonIgnore]
        public override ICollection<UserLogin> Logins { get; }
        //
        // Сводка:
        //     Navigation property for user claims
        [JsonIgnore]
        public override ICollection<UserClaim> Claims { get; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, long> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, long> manager, string AuthenticationType)
        {
            if (AuthenticationType == "Cookies")
            { 
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
                return userIdentity;
            }
            if (AuthenticationType == "Bearer")
            {
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ExternalBearer);
                return userIdentity;
            }
            return null;
        }
    }

    [NotMapped]
    public class UserDTO : BaseEntity
    {
        [Required]
        [Display(Name = "Counter of failed accesses")]
        public int AccessFailedCount { get; set; }
        //
        // Сводка:
        //     Email
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        //
        // Сводка:
        //     True if the email is confirmed, default is false
        [Required]
        [Display(Name = "Email is Confirmed")]
        public bool EmailConfirmed { get; set; }
        //
        // Сводка:
        //     Is lockout enabled for this user
        [Required]
        [Display(Name = "Lockout is Enabled")]
        public bool LockoutEnabled { get; set; }
        //
        // Сводка:
        //     DateTime in UTC when lockout ends, any time in the past is considered not locked
        //     out.
        [Display(Name = "Lockout End Date Utc")]
        public DateTime? LockoutEndDateUtc { get; set; }
        //
        // Сводка:
        //     The salted/hashed form of the user password
        [Display(Name = "Password Hash")]
        public string PasswordHash { get; set; }
        //
        // Сводка:
        //     PhoneNumber for the user
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        //
        // Сводка:
        //     True if the phone number is confirmed, default is false
        [Required]
        [Display(Name = "Phone Number is Confirmed")]
        public bool PhoneNumberConfirmed { get; set; }
        //
        // Сводка:
        //     A random value that should change whenever a users credentials have changed (password
        //     changed, login removed)
        [Display(Name = "Security Stamp")]
        public string SecurityStamp { get; set; }
        //
        // Сводка:
        //     Is two factor enabled for the user
        [Required]
        [Display(Name = "Two Factor ligin is Enabled")]
        public bool TwoFactorEnabled { get; set; }
        //
        // Сводка:
        //     User name
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}
