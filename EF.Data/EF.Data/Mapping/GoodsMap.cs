using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    public class GoodsMap : EntityTypeConfiguration<Goods>
    {
        public GoodsMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired().IsUnicode().IsVariableLength().HasMaxLength(50);
            Property(t => t.Quantity).IsRequired();
            Property(t => t.Info).IsOptional().IsUnicode().IsVariableLength().HasMaxLength(50);
            ToTable("Goods");
        }
    }
}
