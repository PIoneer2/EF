using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EF.Core.Data
{
    public class Transactions : IBaseEntity
    {
        public Transactions()
        {
            this.GoodsInTransaction = new HashSet<GoodsInTransaction>();
        }
        
        public string Description { get; set; }
        public long TranactionTypeId { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        public long Id { get; set; }

        [JsonIgnore]
        public virtual ICollection<GoodsInTransaction> GoodsInTransaction { get; set; }
        public virtual TranactionType TranactionType { get; set; }
        public virtual User Users { get; set; }
    }

    [NotMapped]
    public class TransactionDTO : BaseEntity
    {
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Tranaction Type")]
        public long TranactionTypeId { get; set; }

        [Required]
        [Display(Name = "User")]
        public long UserId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        /*
        public static bool TryParse(string s, out TransactionDTO result)
        {
            result = null;
            string Desc;
            long TrType, UId;
            DateTime Dt;

            var parts = s.Split(',');
            if (parts.Length != 4)
            {
                return false;
            }
            Desc = parts[0];
            if (long.TryParse(parts[1], out TrType) &&
                long.TryParse(parts[2], out UId) &&
                DateTime.TryParse(parts[3], out Dt))
            {
                result = new TransactionDTO()
                {
                    Description = Desc,
                    TranactionTypeId = TrType,
                    UserId = UId,
                    Date = Dt
                };
                return true;
            }
            return false;
        }
        
        class TransactionDTOConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context,
                CultureInfo culture, object value)
            {
                if (value is string)
                {
                    TransactionDTO transaction;
                    if (TransactionDTO.TryParse((string)value, out transaction))
                    {
                        return transaction;
                    }
                }
                return base.ConvertFrom(context, culture, value);
            }
        }*/

    }
}
