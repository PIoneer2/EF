using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public partial class WarehousesPlaces : IBaseEntity
    {
        public WarehousesPlaces()
        {
            this.GoodsInWarehauses = new HashSet<GoodsInWarehauses>();
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

        public string Adress { get; set; }
        public string Place { get; set; }

        public virtual ICollection<GoodsInWarehauses> GoodsInWarehauses { get; set; }
    }
}
