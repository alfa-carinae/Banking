using Banking.DataAccess;
using Banking.DataAccess.Models;
using Banking.Services.Contracts;

namespace Banking.Services.Providers;

public class CustomerService : ICustomerService
{
    private readonly BankingContext _context;
    private readonly IBankService _bankService;
    public CustomerService(BankingContext context, IBankService bankService)
    {
        _context = context;
        _bankService = bankService;
    }

    public Customer AddCustomer(Guid bankId, Customer customer)
    {
        Bank bank = _bankService.GetBank(bankId);
        bank.Customers.Add(customer);
        _context.SaveChanges();
        return customer;
    }

    public IEnumerable<Customer>? GetAllCustomers(Guid bankId)
    {
        return _bankService.GetBank(bankId).Customers;
    }

    public Customer? GetCustomer(Guid customerId)
    {
        return _context.Customers.SingleOrDefault(customer => customer.Id == customerId);
    }

    public Customer UpdateCustomer(Customer customer)
    {
        _context.Customers.Attach(customer);
        _context.SaveChanges();
        return customer;
    }
}
