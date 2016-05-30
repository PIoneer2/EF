using Microsoft.AspNet.Identity.EntityFramework;
using EF.Core.Data;

namespace EF.Data
{
    public class CustomUserStore : UserStore<User, Role, long,
        UserLogin, UserRole, UserClaim>
    {
        public CustomUserStore(EFDbContext context) 
            : base(context)
        {

        }
    }
}
