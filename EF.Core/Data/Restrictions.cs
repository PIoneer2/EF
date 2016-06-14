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
    public class Restrictions : IBaseEntity
    {
        public Restrictions()
        {
            this.RestrictionsSet = new HashSet<RestrictionsSet>();
        }
        
        public long Id { get; set; }
        public string RestrictionName { get; set; }

        [JsonIgnore]
        public virtual ICollection<RestrictionsSet> RestrictionsSet { get; set; }
    }

    [NotMapped]
    public class RestrictionsDTO : BaseEntity
    {
        [Required]
        [Display(Name = "Restriction Name")]
        public string RestrictionName { get; set; }
    }
}
