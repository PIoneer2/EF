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
    public class TypeOfStorageMap : EntityTypeConfiguration<TypeOfStorage>
    {
        public TypeOfStorageMap()
        {
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Type).IsRequired().IsUnicode().IsVariableLength().HasMaxLength(10);
            ToTable("TypeOfStorage");
        }
    }
}
