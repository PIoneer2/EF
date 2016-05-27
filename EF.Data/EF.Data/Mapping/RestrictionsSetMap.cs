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
    public class RestrictionsSetMap : EntityTypeConfiguration<RestrictionsSet>
    {
        public RestrictionsSetMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.RestrictionsId).IsRequired();
            Property(t => t.GoodsId).IsRequired();
            ToTable("RestrictionsSet");
        }
    }
}
