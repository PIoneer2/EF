using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class Permissions : BaseEntity
    {
        public Permissions()
        {
            this.PermissionSet = new HashSet<PermissionSet>();
        }

        public string Name { get; set; }

        public virtual ICollection<PermissionSet> PermissionSet { get; set; }
    }
}
