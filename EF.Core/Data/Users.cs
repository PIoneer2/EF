using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class Users : BaseEntity
    {
        public Users()
        {
            this.Transactions = new HashSet<Transactions>();
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public Int64 RolesId { get; set; }

        public virtual ICollection<Transactions> Transactions { get; set; }
        public virtual Roles Roles { get; set; }
    }
}
