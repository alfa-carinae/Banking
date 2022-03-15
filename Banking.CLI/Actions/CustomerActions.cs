#nullable disable
using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using Banking.Services.Exceptions;
using ConsoleTables;
using Spectre.Console;

namespace Banking.CLI.Actions;

public class CustomerActions
{
    private readonly ITransactionService transactionService;
    private readonly IAccountService accountService;

    public CustomerActions(
        ITransactionService transactionService,
        IAccountService accountService)
    {
        this.transactionService = transactionService;
        this.accountService = accountService;
    }
    public void Parse(string choice)
    {
        switch (choice)
        {
            case "Deposit":
                Deposit();
                break;
            case "Withdraw":
                Withdraw();
                break;
            case "Transfer":
                Transfer();
                break;
            case "ViewTransactionHistory":
                ViewTransactionHistory();
                break;
            default:
                AnsiConsole.MarkupLine("Invalid choice.");
                break;
        }
    }
    public Guid Deposit()
    {
        decimal amount = AnsiConsole.Ask<decimal>("Enter the amount to be deposited:");
        if (Guid.TryParse(AnsiConsole.Ask<string>("Enter your account ID:"), out var accountId) == false)
        {
            AnsiConsole.MarkupLine("Invalid account ID.");
            return Guid.Empty;
        }
        var account = accountService.GetAccount(accountId);
        if (account == null)
        {
            AnsiConsole.MarkupLine("Account not found.");
        }
        return accountService.Deposit(account, amount);
    }
    public Guid Withdraw()
    {
        decimal amount = AnsiConsole.Ask<decimal>("Enter the amount to withdraw:");
        // string currencyCode = InputPrompts.GetCurrencyCode();
        if (Guid.TryParse(AnsiConsole.Ask<string>("Enter your account ID:"), out var accountId) == false)
        {
            AnsiConsole.MarkupLine("Invalid account ID.");
            return Guid.Empty;
        }
        try
        {
            var account = accountService.GetAccount(accountId);
            return accountService.Withdraw(account, amount);
        }
        catch (InsufficientBalanceException)
        {
            Console.WriteLine("Error: Insufficient balance.");
            return Guid.Empty;
        }
    }
    public Guid Transfer()
    {
        if (Guid.TryParse(AnsiConsole.Ask<string>("Enter your account ID:"), out var senderAccountId) == false)
        {
            AnsiConsole.MarkupLine("Invalid account ID.");
            return Guid.Empty;
        }
        if (Guid.TryParse(AnsiConsole.Ask<string>("Enter your account ID:"), out var recipientAccountId) == false)
        {
            AnsiConsole.MarkupLine("Invalid account ID.");
            return Guid.Empty;
        }
        decimal amount = AnsiConsole.Ask<decimal>("Enter the amount to transfer:");
        try
        {
            var senderAccount = accountService.GetAccount(senderAccountId);
            var recipientAccount = accountService.GetAccount(recipientAccountId);
            return accountService.Transfer(senderAccount, recipientAccount, amount);
        }
        catch (InsufficientBalanceException)
        {
            Console.WriteLine("Error: Insufficient balance.");
            return Guid.Empty;
        }
    }
    public void ViewTransactionHistory()
    {
        Console.Write("Enter the number of transactions to display: ");
        int count = Convert.ToInt32(Console.ReadLine());
        if (Guid.TryParse(AnsiConsole.Ask<string>("Enter your account ID:"), out var accountId) == false)
        {
            AnsiConsole.MarkupLine("Invalid account ID.");
            return;
        }
        var account = accountService.GetAccount(accountId);
        IEnumerable<Transaction> transactions = transactionService.GetTransactions(account).Take(count);
        ConsoleTable.From<Transaction>(transactions).Write(Format.Alternative);
        Console.WriteLine();
    }
}
