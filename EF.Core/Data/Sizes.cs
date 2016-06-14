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
    public class Sizes : IBaseEntity
    {
        public Sizes()
        {
            this.Goods = new HashSet<Goods>();
        }
        
        public long Id { get; set; }
        public string Size { get; set; }

        [JsonIgnore]
        public virtual ICollection<Goods> Goods { get; set; }
    }

    [NotMapped]
    public class SizesDTO : BaseEntity
    {
        [Required]
        [Display(Name = "Size description")]
        public string Size { get; set; }
    }
}
