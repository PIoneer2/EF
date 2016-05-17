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
    public class SizesMap : EntityTypeConfiguration<Sizes>
    {
        public SizesMap()
        {
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Size).IsRequired().IsUnicode().IsVariableLength().HasMaxLength(50);
            ToTable("Sizes");
        }
    }
}
