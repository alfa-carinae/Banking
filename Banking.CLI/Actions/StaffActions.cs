#nullable disable
using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using ConsoleTables;
using Spectre.Console;

namespace Banking.CLI.Actions;
public class StaffActions
{
    private readonly ICustomerService customerService;
    private readonly IAccountService accountService;
    private readonly ITransactionService transactionService;
    private readonly ICurrencyService currencyService;
    private readonly IBankService bankService;

    public StaffActions(
        ICustomerService customerService,
        IAccountService accountService,
        ITransactionService transactionService,
        ICurrencyService currencyService,
        IBankService bankService)
    {
        this.customerService = customerService;
        this.accountService = accountService;
        this.transactionService = transactionService;
        this.currencyService = currencyService;
        this.bankService = bankService;
    }
    public void Parse(Guid bankId, string choice)
    {
        switch (choice)
        {
            case "CreateCustomer":
                CreateCustomer(bankId);
                break;
            case "EditCustomerDetails":
                EditCustomerDetails();
                break;
            case "CreateCustomerAccount":
                CreateCustomerAccount();
                break;
            case "DeleteCustomerAccount":
                CloseCustomerAccount();
                break;
            case "ViewAccountTransactionHistory":
                ViewAccountTransactionHistory();
                break;
            case "AddCurrency":
                AddCurrency();
                break;
            case "EditCurrencyRates":
                EditCurrencyRates();
                break;
            case "EditServiceCharges":
                EditServiceCharges();
                break;
            default:
                AnsiConsole.MarkupLine("Invalid choice.");
                break;
        }
    }
    public Guid CreateCustomer(Guid bankId)
    {
        var bank = bankService.GetBank(bankId);
        if (bank == null)
        {
            AnsiConsole.MarkupLine("Invalid bank ID.");
            return Guid.Empty;
        }
        AnsiConsole.MarkupLine("Please provide the customer details.");
        Customer customer = new()
        {
            Name = AnsiConsole.Ask<string>("Enter the customer's name:"),
            Phone = AnsiConsole.Ask<string>("Enter the customer's phone number:"),
            Address = AnsiConsole.Ask<string>("Enter the customer's address:"),
            City = AnsiConsole.Ask<string>("Enter the customer's city:"),
            State = AnsiConsole.Ask<string>("Enter the customer's state:")
        };
        var customerId = customerService.AddCustomer(bank, customer).Id;
        AnsiConsole.MarkupLine($"Customer ID is {customerId}.");

        return customerId;
    }
    public Guid EditCustomerDetails()
    {
        if (Guid.TryParse(AnsiConsole.Ask<string>("Enter the customer's ID:"), out var customerId) == false)
        {
            AnsiConsole.MarkupLine("Invalid customer ID.");
            return Guid.Empty;
        }
        Customer customer = customerService.GetCustomer(customerId);
        AnsiConsole.MarkupLine("Please provide the customer details.");
        customer.Name = AnsiConsole.Ask<string>("Enter the customer's name:");
        customer.Phone = AnsiConsole.Ask<string>("Enter the customer's phone number:");
        customer.Address = AnsiConsole.Ask<string>("Enter the customer's address:");
        customer.City = AnsiConsole.Ask<string>("Enter the customer's city:");
        customer.State = AnsiConsole.Ask<string>("Enter the customer's state:");
        customerService.UpdateCustomer(customer);

        return customerId;
    }
    public Guid CreateCustomerAccount()
    {
        if (Guid.TryParse(AnsiConsole.Ask<string>("Enter the customer's ID:"), out var customerId) == false)
        {
            AnsiConsole.MarkupLine("Invalid ID.");
            return Guid.Empty;
        }
        var customer = customerService.GetCustomer(customerId);
        if (customer == null)
        {
            AnsiConsole.MarkupLine("Invalid customer ID.");
            return Guid.Empty;
        }
        var accountId = accountService.AddAccount(
            customer, new Account() { Balance = AnsiConsole.Ask<decimal>("Enter the opening balance:") }).Id;
        AnsiConsole.MarkupLine($"Customer account ID is {accountId}.");

        return accountId;
    }
    public Guid CloseCustomerAccount()
    {
        if (Guid.TryParse(AnsiConsole.Ask<string>("Enter the customer's account ID:"), out var accountId) == false)
        {
            AnsiConsole.MarkupLine("Invalid account ID.");
            return Guid.Empty;
        }
        accountService.CloseAccount(accountService.GetAccount(accountId));
        AnsiConsole.MarkupLine($"Account {accountId} has been closed.");

        return accountId;
    }
    public void ViewAccountTransactionHistory()
    {
        if (Guid.TryParse(AnsiConsole.Ask<string>("Enter the customer's account ID:"), out var accountId) == false)
        {
            AnsiConsole.MarkupLine("Invalid account ID.");
            return;
        }
        IEnumerable<Transaction> transactions = transactionService.GetTransactions(accountService.GetAccount(accountId))
            .Take(AnsiConsole.Ask<int>("Enter the number of transactions to display:"));
        ConsoleTable.From<Transaction>(transactions).Write(Format.Alternative);
    }
    public Guid AddCurrency()
    {
        var currencyId = currencyService.AddCurrency(
            new Currency()
            {
                Name = AnsiConsole.Ask<string>("Enter the currency's name:"),
                Code = AnsiConsole.Ask<string>("Enter the currency code:"),
                ExchangeRate = AnsiConsole.Ask<decimal>("Enter the currency's exchange rate:")
            }).Id;

        return currencyId;
    }
    public Guid EditCurrencyRates()
    {
        Currency currency;
        if ((currency = currencyService.GetCurrency(AnsiConsole.Ask<string>("Enter the currency code:"))) == null)
        {
            currency.ExchangeRate = AnsiConsole.Ask<decimal>("Enter the new exchange rate:");
            currencyService.UpdateCurrency(currency);
            return currency.Id;
        }
        else
        {
            AnsiConsole.MarkupLine("Invalid currency code.");
            return Guid.Empty;
        }
    }
    public string EditServiceCharges()
    {
        throw new NotImplementedException();
    }
}
