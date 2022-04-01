using Banking.DataAccess.Models;

namespace Banking.Services.Contracts;

public interface ICustomerService
{
    Customer AddCustomer(Bank bank, Customer customer);
    IEnumerable<Customer> GetAllCustomers();
    IEnumerable<Customer> GetAllCustomers(Bank bank);
    Customer? GetCustomer(Guid customerId);
    Customer UpdateCustomer(Customer customer);
}
