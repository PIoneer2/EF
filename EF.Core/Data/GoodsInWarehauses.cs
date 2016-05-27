using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class GoodsInWarehauses : IBaseEntity
    {
        public Int64 GoodsId { get; set; }
        public Int64 WarehousesPlacesId { get; set; }

        //public virtual WarehousesPlaces WarehousesPlaces { get; set; }
        //public virtual Goods Goods { get; set; }
        
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
    }

}
