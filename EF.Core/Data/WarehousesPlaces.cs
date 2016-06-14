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
    public class WarehousesPlaces : IBaseEntity
    {
        public WarehousesPlaces()
        {
            this.GoodsInWarehauses = new HashSet<GoodsInWarehauses>();
        }
        
        public long Id { get; set; }
        public string Adress { get; set; }
        public string Place { get; set; }

        [JsonIgnore]
        public virtual ICollection<GoodsInWarehauses> GoodsInWarehauses { get; set; }
    }

    [NotMapped]
    public class WarehousesPlacesDTO : BaseEntity
    {
        [Required]
        [Display(Name = "Adress")]
        public string Adress { get; set; }

        [Required]
        [Display(Name = "Place number/code")]
        public string Place { get; set; }
    }
}
