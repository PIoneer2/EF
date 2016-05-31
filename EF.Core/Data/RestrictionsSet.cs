using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class RestrictionsSet : IBaseEntity
    {
        public Int64 RestrictionsId { get; set; }
        public Int64 GoodsId { get; set; }
        public long Id { get; set; }

        public virtual Restrictions Restrictions { get; set; }
        public virtual Goods Goods { get; set; }
    }
}
