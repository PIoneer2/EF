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
    public class RestrictionsMap : EntityTypeConfiguration<Restrictions>
    {
        public RestrictionsMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.RestrictionName).IsRequired().IsUnicode().IsVariableLength().HasMaxLength(10);
            ToTable("Restrictions");
        }
    }
}
