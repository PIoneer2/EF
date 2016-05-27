using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    class CustomUserLoginsMap : EntityTypeConfiguration<CustomUserLogin>
    {
        public CustomUserLoginsMap()
        {
            HasKey(r => new { r.AspNetUsersId, r.LoginProvider, r.ProviderKey });
            Property(t => t.AspNetUsersId).IsRequired();
            Property(t => t.LoginProvider).IsUnicode().IsVariableLength().HasMaxLength(128).IsRequired();
            Property(t => t.ProviderKey).IsUnicode().IsVariableLength().HasMaxLength(128).IsRequired();
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            ToTable("AspNetUserLogins");
        }
    }
}
