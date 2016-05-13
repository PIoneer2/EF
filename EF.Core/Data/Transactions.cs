using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class Transactions : BaseEntity
    {
        //
        //Закомментировано для обрезки стартовой базы
        //
        //public Transactions()
        //{
        //    this.GoodsInTransaction = new HashSet<GoodsInTransaction>();
        //}
        //public virtual ICollection<GoodsInTransaction> GoodsInTransaction { get; set; }

        public string Description { get; set; }
        public Int64 TranactionTypeId { get; set; }
        public Int64 UsersId { get; set; }
        public DateTime Date { get; set; }

        public virtual TranactionType TranactionType { get; set; }
        public virtual Users Users { get; set; }
        
    }
}
