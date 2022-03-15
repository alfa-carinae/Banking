#nullable disable
using Banking.DataAccess.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.DataAccess.Models
{
    public class ServiceCharge
    {
        public Guid Id { get; set; }
        [ForeignKey("Bank"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid BankId { get; set; }
        public TransferType TransferType { get; set; }
        public ChargeType ChargeType { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Rate { get; set; }
        public virtual Bank Bank { get; set; }
    }
}
