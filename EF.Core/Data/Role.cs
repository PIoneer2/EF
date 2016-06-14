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
    public class Role : IdentityRole<long, UserRole>, IBaseEntity
    {
        public Role()
        {
            this.Users = new HashSet<UserRole>();
        }

        public Role(string name)
        {
            Name = name;
            this.Users = new HashSet<UserRole>();
        }
        //
        // Сводка:
        //     Role id
        //public override long Id { get; set; }
        //
        // Сводка:
        //     Role name
        //public new string Name { get; set; }

        [JsonIgnore]
        public virtual new ICollection<UserRole> Users { get; }
    }

    [NotMapped]
    public class RoleDTO : BaseEntity
    {
        [Required]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }
}
