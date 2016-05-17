﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public partial class Goods : BaseEntity
    {
        public Goods()
        {
            this.GoodsInWarehauses = new HashSet<GoodsInWarehauses>();
            this.RestrictionsSet = new HashSet<RestrictionsSet>();
            this.GoodsInTransaction = new HashSet<GoodsInTransaction>();
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public Int64 TypeOfStorageId { get; set; }
        public Int64 SizesId { get; set; }
        public string Info { get; set; }

        public virtual ICollection<GoodsInWarehauses> GoodsInWarehauses { get; set; }
        public virtual ICollection<RestrictionsSet> RestrictionsSet { get; set; }
        public virtual ICollection<GoodsInTransaction> GoodsInTransaction { get; set; }
        public virtual TypeOfStorage TypeOfStorage { get; set; }
        public virtual Sizes Sizes { get; set; }

    }
}