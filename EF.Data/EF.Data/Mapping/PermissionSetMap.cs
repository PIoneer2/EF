using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    public class PermissionSetMap : EntityTypeConfiguration<PermissionSet>
    {
        public PermissionSetMap()
        {
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.RolesId).IsRequired();
            Property(t => t.PermissionsId).IsRequired();
            ToTable("PermissionSet");
        }
    }
}
