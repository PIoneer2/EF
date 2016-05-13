using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class PermissionSet : BaseEntity
    {
        public Int64 RolesId { get; set; }
        public Int64 PermissionsId { get; set; }

        public virtual Roles Roles { get; set; }        
        public virtual Permissions Permissions { get; set; }
    }
}
