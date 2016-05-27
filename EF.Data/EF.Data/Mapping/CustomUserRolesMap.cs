using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    class CustomUserRolesMap : EntityTypeConfiguration<CustomUserRole>
    {
        public CustomUserRolesMap()
        {
            HasKey(t => new { t.CustomRoleId, t.AspNetUsersId });
            Property(t => t.AspNetUsersId).IsRequired();
            Property(t => t.CustomRoleId).IsRequired();
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            ToTable("AspNetUserRoles");
        }
    }
}
