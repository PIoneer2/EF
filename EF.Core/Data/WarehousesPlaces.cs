using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public partial class WarehousesPlaces : BaseEntity
    {
        public WarehousesPlaces()
        {
            this.GoodsInWarehauses = new HashSet<GoodsInWarehauses>();
        }
        
        public string Adress { get; set; }
        public string Place { get; set; }

        public virtual ICollection<GoodsInWarehauses> GoodsInWarehauses { get; set; }
    }
}
