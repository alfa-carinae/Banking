#nullable disable
namespace Banking.DataAccess.Models
{
    public class Transfer : Transaction
    {
        public Guid SenderAccountId { get; set; }
        public Guid RecipientAccountId { get; set; }
    }
}
