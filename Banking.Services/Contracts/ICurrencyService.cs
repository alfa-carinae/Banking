using Banking.DataAccess.Models;

namespace Banking.Services.Contracts;

public interface ICurrencyService
{
    Currency AddCurrency(Currency currency);
    Currency? GetCurrency(string currencyCode);
    IEnumerable<Currency> GetCurrencies();
    Currency UpdateCurrency(Currency currency);
}
