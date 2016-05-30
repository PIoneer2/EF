using Microsoft.AspNet.Identity.EntityFramework;
using EF.Core.Data;

namespace EF.Data
{
    public class CustomRoleStore : RoleStore<Role, long, UserRole>
    {
        public CustomRoleStore(EFDbContext context)
            : base(context)
        {

        }
    }
}
