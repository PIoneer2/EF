using Microsoft.AspNet.Identity.EntityFramework;
using EF.Core.Data;
using EF.Core;
using System.Data.Entity;

namespace EF.Core
{
    public class CustomRoleStore : RoleStore<Role, long, UserRole>, ICustomRoleStore
    {
        public CustomRoleStore(DbContext context)
            : base(context)
        {

        }
    }
}
