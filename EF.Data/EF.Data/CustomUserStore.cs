using Microsoft.AspNet.Identity.EntityFramework;
using EF.Core.Data;
using EF.Core;

namespace EF.Data
{
    public class CustomUserStore : UserStore<User, Role, long,
        UserLogin, UserRole, UserClaim>, ICustomUserStore
    {
        public CustomUserStore(EFDbContext context) 
            : base(context)
        {
            
        }
    }
}
