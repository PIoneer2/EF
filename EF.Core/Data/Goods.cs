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
    public partial class Goods : IBaseEntity
    {
        public Goods()
        {
            this.GoodsInWarehauses = new HashSet<GoodsInWarehauses>();
            this.RestrictionsSet = new HashSet<RestrictionsSet>();
            this.GoodsInTransaction = new HashSet<GoodsInTransaction>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public long TypeOfStorageId { get; set; }
        public long SizesId { get; set; }
        public string Info { get; set; }

        [JsonIgnore]
        public virtual ICollection<GoodsInWarehauses> GoodsInWarehauses { get; set; }
        [JsonIgnore]
        public virtual ICollection<RestrictionsSet> RestrictionsSet { get; set; }
        [JsonIgnore]
        public virtual ICollection<GoodsInTransaction> GoodsInTransaction { get; set; }
        public virtual TypeOfStorage TypeOfStorage { get; set; }
        public virtual Sizes Sizes { get; set; }
    }


    [NotMapped]
    public class GoodDTO : BaseEntity
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Good name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type Of Storage Id")]
        public long TypeOfStorageId { get; set; }

        [Required]
        [Display(Name = "Sizes Id")]
        public long SizesId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Info, optional")]
        public string Info { get; set; }
    }
}
