﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class Restrictions : IBaseEntity
    {
        public Restrictions()
        {
            this.RestrictionsSet = new HashSet<RestrictionsSet>();
        }
        
        public long Id { get; set; }
        public string RestrictionName { get; set; }

        public virtual ICollection<RestrictionsSet> RestrictionsSet { get; set; }
    }
}
