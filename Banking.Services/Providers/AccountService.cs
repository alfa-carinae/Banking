using Banking.DataAccess;
using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using Banking.Services.Exceptions;

namespace Banking.Services.Providers;

public class AccountService : IAccountService
{
    private readonly BankingContext _context;
    private readonly ITransactionService _transactionService;
    public AccountService(BankingContext context, ITransactionService transactionService)
    {
        _context = context;
        _transactionService = transactionService;
    }

    public Account? GetAccount(Guid accountId) => _context.Accounts.SingleOrDefault(account => account.Id == accountId);
    public IEnumerable<Account> GetAllAccounts() => _context.Accounts;
    public IEnumerable<Account> GetAllAccounts(Customer customer) => customer.Accounts;
    public Account AddAccount(Customer customer, Account account)
    {
        customer.Accounts.Add(account);
        _context.SaveChanges();
        return account;
    }
    public Account UpdateAccount(Account account)
    {
        _context.Accounts.Attach(account);
        _context.SaveChanges();
        return account;
    }
    public void CloseAccount(Account account)
    {
        account.Status = DataAccess.Enums.AccountStatus.Closed;
        _context.SaveChanges();
    }

    public Guid Deposit(Account account, decimal amount)
    {
        account.Balance += amount;
        Deposit transaction = new()
        {
            Amount = amount
        };
        Guid transactionId = _transactionService.AddTransaction(account, transaction).Id;
        return transactionId;
    }

    public Guid Withdraw(Account account, decimal amount)
    {
        if (account.Balance - amount <= 0)
        {
            throw new InsufficientBalanceException();
        }
        account.Balance -= amount;
        Withdrawl transaction = new()
        {
            Amount = amount
        };
        Guid transactionId = _transactionService.AddTransaction(account, transaction).Id;
        return transactionId;
    }

    public Guid Transfer(Account senderAccount, Account recipientAccount, decimal amount)
    {
        if (senderAccount.Balance - amount <= 0)
        {
            throw new InsufficientBalanceException();
        }
        senderAccount.Balance -= amount;
        recipientAccount.Balance += amount;
        Transfer transaction = new()
        {
            Amount = amount,
            SenderAccountId = senderAccount.Id,
            RecipientAccountId = recipientAccount.Id
        };
        Guid transactionId = _transactionService.AddTransaction(senderAccount, transaction).Id;
        _transactionService.AddTransaction(recipientAccount, transaction);
        return transactionId;
    }
}
