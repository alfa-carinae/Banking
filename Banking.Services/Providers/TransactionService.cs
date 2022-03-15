using Banking.DataAccess;
using Banking.DataAccess.Models;
using Banking.Services.Contracts;

namespace Banking.Services.Providers;

public class TransactionService : ITransactionService
{
    private readonly BankingContext _context;
    public TransactionService(BankingContext context)
    {
        _context = context;
    }
    public Transaction GetTransaction(Guid transactionId) =>
        _context.Transactions.Single(transaction => transaction.Id == transactionId);
    public IEnumerable<Transaction> GetTransactions(Account account) => account.Transactions;
    public Transaction AddTransaction(Account account, Transaction transaction)
    {
        account.Transactions.Add(transaction);
        _context.SaveChanges();
        return transaction;
    }
    public Transaction UpdateTransaction(Transaction transaction)
    {
        _context.Transactions.Attach(transaction);
        _context.SaveChanges();
        return transaction;
    }
}
