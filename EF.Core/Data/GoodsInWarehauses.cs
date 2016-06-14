using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class GoodsInWarehauses : IBaseEntity
    {
        public long Id { get; set; }
        public long GoodsId { get; set; }
        public long WarehousesPlacesId { get; set; }

        public virtual WarehousesPlaces WarehousesPlaces { get; set; }
        public virtual Goods Goods { get; set; }
    }

    [NotMapped]
    public class GoodsInWarehausesDTO : BaseEntity
    {
        [Required]
        [Display(Name = "Goods Id")]
        public long GoodsId { get; set; }

        [Required]
        [Display(Name = "Warehouses Places Id")]
        public long WarehousesPlacesId { get; set; }
    }
}
