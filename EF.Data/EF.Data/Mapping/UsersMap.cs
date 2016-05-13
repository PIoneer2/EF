using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    public class UsersMap : EntityTypeConfiguration<Users>
    {
        public UsersMap()
        {
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Login).IsRequired().IsUnicode().IsVariableLength();
            Property(t => t.Password).IsRequired().IsUnicode().IsMaxLength();
            Property(t => t.RolesId).IsRequired();
            ToTable("Users");
        }
    }
}
