using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public partial class GoodsInTransaction : IBaseEntity
    {
        
        public int Quantity { get; set; }
        public Int64 TransactionsId { get; set; }
        public Int64 GoodsId { get; set; }

        //public virtual Transactions Transactions { get; set; }
        //public virtual Goods Goods { get; set; }
        
        public long Id { get; set; }
    }

}
