using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    public class RolesMap : EntityTypeConfiguration<Role>
    {
        public RolesMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsUnicode().IsVariableLength().HasMaxLength(256).IsRequired();
            ToTable("Roles");
        }
    }
}
