using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class TranactionType : IBaseEntity
    {
        public TranactionType()
        {
            this.Transactions = new HashSet<Transactions>();
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

        public string Name { get; set; }

        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
