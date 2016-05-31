using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EF.Core;
using EF.Core.Data;

namespace EF.Core.Data
{
    public class UserLogin : IdentityUserLogin<long>, IBaseEntity
    {
        //
        // Сводка:
        //     The login provider for the login (i.e. facebook, google)
        public override string LoginProvider { get; set; }
        //
        // Сводка:
        //     Key representing the login for the provider
        public override string ProviderKey { get; set; }
        //
        // Сводка:
        //     User Id for the user who owns this login
        public override long UserId { get; set; }   
        public long Id { get; set; }                

        public UserLogin()
        {

        }
    }
}
