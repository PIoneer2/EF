using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    public class WarehousesPlacesMap : EntityTypeConfiguration<WarehousesPlaces>
    {
        public WarehousesPlacesMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Adress).IsRequired();
            Property(t => t.Place).IsRequired();
            ToTable("WarehousesPlaces");
        }
    }
}
