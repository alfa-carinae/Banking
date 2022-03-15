using Banking.DataAccess.Models;

namespace Banking.Services.Contracts;

public interface ITransactionService
{
    Transaction AddTransaction(Account account, Transaction transaction);
    Transaction GetTransaction(Guid transactionId);
    IEnumerable<Transaction> GetTransactions(Account account);
    Transaction UpdateTransaction(Transaction transaction);
}
