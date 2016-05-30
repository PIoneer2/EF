using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    class UserRolesMap : EntityTypeConfiguration<UserRole>
    {
        public UserRolesMap()
        {
            HasKey(t => new { t.RoleId, t.UserId });
            Property(t => t.UserId).IsRequired();
            Property(t => t.RoleId).IsRequired();
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            ToTable("UserRoles");
        }
    }
}
