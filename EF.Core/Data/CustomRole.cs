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
    public class CustomRole : IdentityRole<long, CustomUserRole>, IBaseEntity
    {
        public new string Name { get; set; }
        public CustomRole()
        {
            this.CustomUserRole = new HashSet<CustomUserRole>();
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
        public virtual ICollection<CustomUserRole> CustomUserRole { get; set; }

        public CustomRole(string name)
        {
            Name = name;
            this.CustomUserRole = new HashSet<CustomUserRole>();
        }
    }
}
