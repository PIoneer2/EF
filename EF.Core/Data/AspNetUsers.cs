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
    public class AspNetUsers : IdentityUser<long, CustomUserLogin, CustomUserRole, CustomUserClaim>, IBaseEntity
    {
        public AspNetUsers()
        {
            this.Transactions = new HashSet<Transactions>();
            this.CustomUserRole = new HashSet<CustomUserRole>();
            this.CustomUserLogin = new HashSet<CustomUserLogin>();
            this.CustomUserClaim = new HashSet<CustomUserClaim>();

        }

        public new long Id
        {
            get
            {
                return Id;
            }

            set
            {
                Id = value;
            }
        }
        public virtual ICollection<Transactions> Transactions { get; set; }
        public virtual ICollection<CustomUserRole> CustomUserRole { get; set; }
        public virtual ICollection<CustomUserLogin> CustomUserLogin { get; set; }
        public virtual ICollection<CustomUserClaim> CustomUserClaim { get; set; }
              
    }
}
