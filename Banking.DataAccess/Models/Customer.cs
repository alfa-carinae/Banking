#nullable disable
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.DataAccess.Models
{
    public class Customer : User
    {
        [ForeignKey("Bank"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid BankId { get; set; }
        public ICollection<Account> Accounts { get; set; } = new Collection<Account>();
        public virtual Bank Bank { get; set; }
    }
}
