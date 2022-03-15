using Banking.CLI;
using Banking.CLI.Actions;
using Banking.DataAccess;
using Banking.Services.Contracts;
using Banking.Services.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args).ConfigureServices((context, services) =>
{
    services.AddDbContext<BankingContext>(options =>
        options.UseSqlServer(context.Configuration.GetSection("ConnectionStrings")["electron"]));
    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<IBankService, BankService>();
    services.AddScoped<ICustomerService, CustomerService>();
    services.AddScoped<IStaffService, StaffService>();
    services.AddScoped<ITransactionService, TransactionService>();
    services.AddScoped<ICurrencyService, CurrencyService>();
    services.AddScoped<CustomerActions>();
    services.AddScoped<StaffActions>();
    services.AddScoped<AdministratorActions>();
    services.AddScoped<RootActions>();
    services.AddScoped<IConsoleInstance, ConsoleInstance>();
}).Build();
var context = host.Services.GetRequiredService<BankingContext>();
Console.WriteLine($"There are currently {context.Banks.Count()} bank(s) in the database.");
var instance = host.Services.GetRequiredService<IConsoleInstance>();
while (true)
{
    instance.BankId = context.Banks.First().Id;
    instance.Start();
}