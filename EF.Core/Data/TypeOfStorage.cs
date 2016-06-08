using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class TypeOfStorage : IBaseEntity
    {
        public TypeOfStorage()
        {
            this.Goods = new HashSet<Goods>();
        }
        
        public long Id { get; set; }
        public string Type { get; set; }

        [JsonIgnore]
        public virtual ICollection<Goods> Goods { get; set; }
    }
}
