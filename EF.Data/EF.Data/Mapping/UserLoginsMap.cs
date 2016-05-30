using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    class UserLoginsMap : EntityTypeConfiguration<UserLogin>
    {
        public UserLoginsMap()
        {
            HasKey(r => new { r.UserId, r.LoginProvider, r.ProviderKey });
            Property(t => t.UserId).IsRequired();
            Property(t => t.LoginProvider).IsUnicode().IsVariableLength().HasMaxLength(128).IsRequired();
            Property(t => t.ProviderKey).IsUnicode().IsVariableLength().HasMaxLength(128).IsRequired();
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            ToTable("Logins");
        }
    }
}
