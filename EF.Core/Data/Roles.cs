using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class Roles : BaseEntity
    {
        public Roles()
        {
            this.PermissionSet = new HashSet<PermissionSet>();
            this.Users = new HashSet<Users>();
        }

        public string Name { get; set; }

        public virtual ICollection<PermissionSet> PermissionSet { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
