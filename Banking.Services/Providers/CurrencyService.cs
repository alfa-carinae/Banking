using Banking.DataAccess;
using Banking.DataAccess.Models;
using Banking.Services.Contracts;

namespace Banking.Services.Providers;

public class CurrencyService : ICurrencyService
{
    private readonly BankingContext _context;
    public CurrencyService(BankingContext context) => _context = context;
    public Currency AddCurrency(Currency currency)
    {
        _context.Currencies.Add(currency);
        _context.SaveChanges();
        return currency;
    }
    public IEnumerable<Currency> GetCurrencies() => _context.Currencies;
    public Currency? GetCurrency(string currencyCode) => _context.Currencies.FirstOrDefault(c => c.Code == currencyCode);
    public Currency UpdateCurrency(Currency currency)
    {
        _context.Currencies.Attach(currency);
        _context.SaveChanges();
        return currency;
    }
}