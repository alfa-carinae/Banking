#nullable disable
using Banking.DataAccess.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.DataAccess.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        [ForeignKey("Customer"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CustomerId { get; set; }
        public DateTime OpenDate { get; } = DateTime.Now;
        [Column(TypeName = "decimal")]
        public decimal Balance { get; set; } = 0;
        public AccountStatus Status { get; set; } = AccountStatus.Active;
        public List<Transaction> Transactions { get; } = new List<Transaction>();
        public virtual Customer Customer { get; set; }
    }
}