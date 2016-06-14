using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    [NotMapped]
    public class TypeOfStorageDTO : BaseEntity
    {
        [Required]
        [Display(Name = "Name of storage type")]
        public string Type { get; set; }
    }
}
