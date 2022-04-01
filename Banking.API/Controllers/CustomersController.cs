using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService customerService;
    private readonly IBankService bankService;

    public CustomersController(ICustomerService customerService, IBankService bankService)
    {
        this.customerService = customerService;
        this.bankService = bankService;
    }
    [HttpGet]
    public IEnumerable<Customer>? GetCustomrs()
    {
        return customerService.GetAllCustomers();
    }
    [HttpGet("{id}")]
    public IActionResult GetCustomers(string id)
    {
        Guid.TryParse(id, out var bankId);
        if (bankId == Guid.Empty)
        {
            return BadRequest();
        }
        var bank = bankService.GetBank(bankId);
        if (bank == null)
        {
            return NotFound();
        }
        var customers = customerService.GetAllCustomers(bank);
        if (customers == null)
        {
            return BadRequest();
        }
        return Ok(customers);
    }
    [HttpGet("{id}")]
    public IActionResult GetCustomer(string id)
    {
        Guid.TryParse(id, out var customerId);
        if (customerId == Guid.Empty)
        {
            return BadRequest();
        }
        var customer = customerService.GetCustomer(customerId);
        if (customer == null)
        {
            return BadRequest();
        }
        return Ok(customer);
    }
    [HttpPost("{id}")]
    public IActionResult AddCustomer(string id, [FromBody] Customer customer)
    {
        Guid.TryParse(id, out var bankId);
        if (bankId == Guid.Empty)
        {
            return BadRequest();
        }
        var bank = bankService.GetBank(bankId);
        if (bank == null)
        {
            return BadRequest();
        }
        customerService.AddCustomer(bank, customer);
        return Ok();
    }
    [HttpPost("{cid}")]
    public IActionResult UpdateCustomer(string cid, [FromBody] Customer customer)
    {
        Guid.TryParse(cid, out var customerId);
        if (customerId == Guid.Empty)
        {
            return BadRequest();
        }
        var cust = customerService.GetCustomer(customerId);
        if (cust == null)
        {
            return NotFound();
        }
        cust.Name = customer.Name ?? cust.Name;
        cust.Phone = customer.Phone ?? cust.Phone;
        cust.Address = customer.Address ?? cust.Address;
        cust.City = customer.City ?? cust.City;
        cust.State = customer.State ?? cust.State;
        customerService.UpdateCustomer(cust);
        return Ok();
    }
}
