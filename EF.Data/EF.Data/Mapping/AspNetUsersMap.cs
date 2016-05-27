using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    public class AspNetUsersMap : EntityTypeConfiguration<AspNetUsers>
    {
        public AspNetUsersMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Email).IsUnicode().IsVariableLength().IsOptional();
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
            ToTable("AspNetUsers");
        }
    }
}
