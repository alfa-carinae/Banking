using Banking.DataAccess.Models;

namespace Banking.Services.Contracts;

public interface ICustomerService
{
    Customer AddCustomer(Guid bankId, Customer customer);
    IEnumerable<Customer>? GetAllCustomers(Guid bankId);
    Customer? GetCustomer(Guid customerId);
    Customer UpdateCustomer(Customer customer);
}
