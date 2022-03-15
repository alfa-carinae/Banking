using Banking.DataAccess.Models;

namespace Banking.Services.Contracts;

public interface IBankService
{
    Bank AddBank(Bank bank);
    Bank GetBank(Guid bankId);
    IEnumerable<Bank> GetBanks();
    Bank UpdateBank(Bank bank);
}
