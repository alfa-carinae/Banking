#nullable disable
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.DataAccess.Models
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        [Column(TypeName = "decimal")]
        public decimal ExchangeRate { get; set; } // WRT INR
    }
}
