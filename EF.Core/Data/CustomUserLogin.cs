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
    public class CustomUserLogin : IdentityUserLogin<long>, IBaseEntity
    {
        public CustomUserLogin()
        {
            
        }

        public long Id
        {
            get
            {
                return Id;
            }

            set
            {
                Id = value;
            }
        }

        public long AspNetUsersId { get; set; }
        public new string LoginProvider { get; set; }
        public new string ProviderKey { get; set; }
    }
}
