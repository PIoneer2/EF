using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
    }

    public interface IBaseEntity
    {
        long Id { get; set; }
    }
}
