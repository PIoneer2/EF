using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    public class UsersMap : EntityTypeConfiguration<EF.Core.Data.User>
    {
        public UsersMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Email).IsUnicode().IsVariableLength().IsRequired();
            Property(t => t.EmailConfirmed).IsRequired();
            Property(t => t.PasswordHash).IsUnicode().IsVariableLength().IsMaxLength().IsOptional();
            Property(t => t.SecurityStamp).IsUnicode().IsVariableLength().IsMaxLength().IsOptional();
            Property(t => t.PhoneNumber).IsUnicode().IsVariableLength().IsMaxLength().IsOptional();
            Property(t => t.PhoneNumberConfirmed).IsRequired();
            Property(t => t.TwoFactorEnabled).IsRequired();
            Property(t => t.LockoutEndDateUtc).IsOptional();
            Property(t => t.LockoutEnabled).IsRequired();
            Property(t => t.AccessFailedCount).IsRequired();
            Property(t => t.UserName).IsUnicode().IsVariableLength().IsRequired();
            ToTable("Users");
        }
    }
}
