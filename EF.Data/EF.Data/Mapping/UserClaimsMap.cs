using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    class UserClaimsMap : EntityTypeConfiguration<UserClaim>
    {
        public UserClaimsMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.UserId).IsRequired();
            Property(t => t.ClaimType).IsUnicode().IsVariableLength().IsMaxLength().IsOptional();
            Property(t => t.ClaimValue).IsUnicode().IsVariableLength().IsMaxLength().IsOptional();
            ToTable("Claims");
        }
    }
}
