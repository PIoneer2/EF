using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    public class CustomRolesMap : EntityTypeConfiguration<CustomRole>
    {
        public CustomRolesMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsUnicode().IsVariableLength().HasMaxLength(256).IsRequired();
            ToTable("AspNetRoles");
        }
    }
}
