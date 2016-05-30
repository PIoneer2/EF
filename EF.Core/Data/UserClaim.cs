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
    public class UserClaim : IdentityUserClaim<long>, IBaseEntity
    {

        //
        // Сводка:
        //     Claim type
        public override string ClaimType { get; set; }
        //
        // Сводка:
        //     Claim value
        public override string ClaimValue { get; set; }
        //
        // Сводка:
        //     Primary key
        public new long Id { get; set; }
        //
        // Сводка:
        //     User Id for the user who owns this login
        public override long UserId { get; set; }

        public UserClaim()
        {

        }
    }
}
