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
    public class Role : IdentityRole<long, UserRole>, IBaseEntity
    {
        //
        // Сводка:
        //     Role id
        public new long Id { get; set; }
        //
        // Сводка:
        //     Role name
        public new string Name { get; set; }

        public virtual new ICollection<UserRole> Users { get; }

        public Role()
        {
            this.Users = new HashSet<UserRole>();
        }

        public Role(string name)
        {
            Name = name;
            this.Users = new HashSet<UserRole>();
        }
    }
}
