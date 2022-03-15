#nullable disable
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.DataAccess.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        [ForeignKey("Account"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AccountId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal")]
        public decimal Amount { get; set; }
        public virtual Account Account { get; set; }
    }
}