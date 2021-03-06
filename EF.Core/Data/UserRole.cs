﻿using System;
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
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EF.Core.Data
{
    public class UserRole : IdentityUserRole<long>, IBaseEntity
    {
        public long Id { get; set; }
        //
        // Сводка:
        //     RoleId for the role
        public override long RoleId { get; set; }
        //
        // Сводка:
        //     UserId for the user that is in the role
        public override long UserId { get; set; }

        public UserRole()
        {

        }
    }

    [NotMapped]
    public class UserRoleDTO : BaseEntity
    {
        [Required]
        [Display(Name = "Role Id")]
        public long RoleId { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public long UserId { get; set; }
    }
}
