using Microsoft.AspNet.Identity.EntityFramework;
using EF.Core.Data;
using EF.Core;
using System.Data.Entity;

namespace EF.Core
{
    public class CustomUserStore : UserStore<User, Role, long,
        UserLogin, UserRole, UserClaim>, ICustomUserStore
    {
        public CustomUserStore(DbContext context) 
            : base(context)
        {
            
        }
    }
}
