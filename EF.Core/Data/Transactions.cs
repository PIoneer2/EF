using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EF.Core.Data
{
    public class Transactions : IBaseEntity
    {
        public Transactions()
        {
            this.GoodsInTransaction = new HashSet<GoodsInTransaction>();
        }
        
        public string Description { get; set; }
        public long TranactionTypeId { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        public long Id { get; set; }

        //[JsonIgnore]
        public virtual ICollection<GoodsInTransaction> GoodsInTransaction { get; set; }
        public virtual TranactionType TranactionType { get; set; }
        public virtual User Users { get; set; }
    }

    [NotMapped]
    public class TransactionDTO : BaseEntity
    {
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Tranaction Type")]
        public long TranactionTypeId { get; set; }

        [Required]
        [Display(Name = "User")]
        public long UserId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        public TranactionType TranactionType { get; set; }
        public User Users { get; set; }
    }
}
