using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class RestrictionsSet : IBaseEntity
    {
        public long RestrictionsId { get; set; }
        public long GoodsId { get; set; }
        public long Id { get; set; }

        public virtual Restrictions Restrictions { get; set; }
        public virtual Goods Goods { get; set; }
    }

    [NotMapped]
    public class RestrictionsSetDTO : BaseEntity
    {
        [Required]
        [Display(Name = "Restrictions Id")]
        public long RestrictionsId { get; set; }

        [Required]
        [Display(Name = "Goods Id")]
        public long GoodsId { get; set; }
    }
}
