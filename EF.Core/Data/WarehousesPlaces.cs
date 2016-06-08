using Newtonsoft.Json;
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
        
        public long Id { get; set; }
        public string Adress { get; set; }
        public string Place { get; set; }

        [JsonIgnore]
        public virtual ICollection<GoodsInWarehauses> GoodsInWarehauses { get; set; }
    }
}
