using Microsoft.AspNet.Identity.EntityFramework;
using EF.Core.Data;
using EF.Core;

namespace EF.Data
{
    public class CustomRoleStore : RoleStore<Role, long, UserRole>, ICustomRoleStore
    {
        public CustomRoleStore(EFDbContext context)
            : base(context)
        {

        }
    }
}
