using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class TranactionType : BaseEntity
    {
        public TranactionType()
        {
            this.Transactions = new HashSet<Transactions>();
        }

        public string Name { get; set; }

        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
