using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class Sizes : BaseEntity
    {
        public Sizes()
        {
            this.Goods = new HashSet<Goods>();
        }

        public string Size { get; set; }

        public virtual ICollection<Goods> Goods { get; set; }
    }
}
