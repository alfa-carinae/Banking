#nullable disable
using Banking.DataAccess.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.DataAccess.Models
{
    public class Staff : User
    {
        [ForeignKey("Bank"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid BankId { get; set; }
        public Clearance Clearance { get; set; }
        public virtual Bank Bank { get; set; }
    }
}
