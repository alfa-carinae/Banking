#nullable disable
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.DataAccess.Models
{
    public class Bank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BranchName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public ICollection<ServiceCharge> ServiceCharges { get; set; } = new Collection<ServiceCharge>();
        public ICollection<Staff> Staff { get; set; } = new Collection<Staff>();
        public ICollection<Customer> Customers { get; set; } = new Collection<Customer>();
    }
}
