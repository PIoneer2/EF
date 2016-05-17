using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    public class GoodsInTransactionMap : EntityTypeConfiguration<GoodsInTransaction>
    {
        public GoodsInTransactionMap()
        {
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Quantity).IsRequired();
            Property(t => t.TransactionsId).IsRequired();
            Property(t => t.GoodsId).IsRequired();
            ToTable("GoodsInTransaction");
        }
    }
}
