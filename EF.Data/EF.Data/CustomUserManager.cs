using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.Core;
using Microsoft.AspNet.Identity;
using EF.Core.Data;

namespace EF.Data
{
    public class CustomUserManager : UserManager<User, long>, IUserManager
    {
        public CustomUserManager(CustomUserStore store) 
            : base (store)
        {

        }

        async Task<User> IUserManager.FindByIdAsync(long userId)
        {
            return await this.FindByIdAsync(userId);
        }
    }
}
