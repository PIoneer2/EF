using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EF.Core.Data
{
    public class Transactions : IBaseEntity
    {
        public Transactions()
        {
            this.GoodsInTransaction = new HashSet<GoodsInTransaction>();
        }
        
        public string Description { get; set; }
        public long TranactionTypeId { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        
        public long Id { get; set; }
        public virtual ICollection<GoodsInTransaction> GoodsInTransaction { get; set; }
    }
}
