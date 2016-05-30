using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
    public class TransactionsMap : EntityTypeConfiguration<Transactions>
    {
        public TransactionsMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.UserId).IsRequired();
            Property(t => t.Description).IsOptional().IsUnicode().IsVariableLength().IsMaxLength();
            Property(t => t.TranactionTypeId).IsRequired();
            Property(t => t.Date).IsRequired();
            ToTable("Transactions");
        }
    }
}
