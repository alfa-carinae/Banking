using Banking.DataAccess;
using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using Banking.Services.Exceptions;

namespace Banking.Services.Providers;

public class BankService : IBankService
{
    private readonly BankingContext _context;
    public BankService(BankingContext context) => _context = context;
    public Bank? GetBank(Guid bankId)
    {
        return _context.Banks.SingleOrDefault(bank => bank.Id == bankId);
    }
    public IEnumerable<Bank> GetBanks() => _context.Banks;
    public Bank AddBank(Bank bank)
    {
        _context.Banks.Add(bank);
        _context.SaveChanges();
        return bank;
    }
    public Bank UpdateBank(Bank bank)
    {
        _context.Banks.Attach(bank);
        _context.SaveChanges();
        return bank;
    }
}
