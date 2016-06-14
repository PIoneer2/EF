using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public partial class GoodsInTransaction : IBaseEntity
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public long TransactionsId { get; set; }
        public long GoodsId { get; set; }

        public virtual Transactions Transactions { get; set; }
        public virtual Goods Goods { get; set; }
    }

    [NotMapped]
    public class GoodsInTransactionDTO : BaseEntity
    {
        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Transactions Id")]
        public long TransactionsId { get; set; }

        [Required]
        [Display(Name = "Goods Id")]
        public long GoodsId { get; set; }
    }
}
