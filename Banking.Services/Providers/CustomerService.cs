using Banking.DataAccess;
using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using Banking.Services.Exceptions;

namespace Banking.Services.Providers;

public class CustomerService : ICustomerService
{
    private readonly BankingContext _context;

    public CustomerService(BankingContext context)
    {
        _context = context;
    }

    public Customer AddCustomer(Bank bank, Customer customer) // TODO: Pass bank object directly.
    {
        bank.Customers.Add(customer);
        _context.SaveChanges();
        return customer;
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        return _context.Customers;
    }

    public IEnumerable<Customer> GetAllCustomers(Bank bank)
    {
        return bank.Customers;
    }

    public Customer? GetCustomer(Guid customerId)
    {
        return _context.Customers.SingleOrDefault(customer => customer.Id == customerId);
    }

    public Customer UpdateCustomer(Customer customer)
    {
        // TODO: Check the passed object.
        _context.Customers.Attach(customer);
        _context.SaveChanges();
        return customer;
    }
}
