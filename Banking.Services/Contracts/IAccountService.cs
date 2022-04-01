using Banking.DataAccess.Models;

namespace Banking.Services.Contracts;

public interface IAccountService
{
    Account AddAccount(Customer customer, Account account);
    Account? GetAccount(Guid accountId);
    IEnumerable<Account> GetAllAccounts();
    IEnumerable<Account> GetAllAccounts(Customer customer);
    Account UpdateAccount(Account account);
    void CloseAccount(Account account);
    Guid Deposit(Account account, decimal amount);
    Guid Withdraw(Account account, decimal amount);
    Guid Transfer(Account senderAccount, Account recipientAccount, decimal amount);
}
