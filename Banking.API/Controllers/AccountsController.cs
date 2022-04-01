using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using Banking.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountsController : Controller
{
    private readonly IAccountService accountService;
    private readonly ICustomerService customerService;

    public AccountsController(IAccountService accountService, ICustomerService customerService)
    {
        this.accountService = accountService;
        this.customerService = customerService;
    }
    [HttpGet]
    public IActionResult GetAccounts()
    {
        return Ok(accountService.GetAllAccounts());
    }
    [HttpGet("{cid}")]
    public IActionResult GetAccountsByCustomer(string cid)
    {
        Guid.TryParse(cid, out var customerId);
        if (customerId == Guid.Empty)
        {
            return BadRequest();
        }
        var customer = customerService.GetCustomer(customerId);
        if (customer == null)
        {
            return NotFound();
        }
        var accounts = accountService.GetAllAccounts(customer);
        if (accounts == null)
        {
            return NotFound();
        }
        return Ok(accounts);
    }
    [HttpGet("{acid}")]
    public IActionResult GetAccount(string acid)
    {
        Guid.TryParse(acid, out var accountId);
        if (accountId == Guid.Empty)
        {
            return BadRequest();
        }
        var account = accountService.GetAccount(accountId);
        if (account == null)
        {
            return BadRequest();
        }
        return Ok(account);
    }
    [HttpPost("Customer/{cid}")]
    public IActionResult AddAccount(string cid, [FromBody] Account account)
    {
        Guid.TryParse(cid, out var customerId);
        if (customerId == Guid.Empty)
        {
            return BadRequest();
        }
        var customer = customerService.GetCustomer(customerId);
        if (customer == null)
        {
            return BadRequest();
        }
        accountService.AddAccount(customer, account);
        return Ok();
    }
    [HttpPost("{acid}")]
    public IActionResult UpdateAccount(string acid, [FromBody] Account account)
    {
        Guid.TryParse(acid, out var accountId);
        if (accountId == Guid.Empty)
        {
            return BadRequest();
        }
        var acc = accountService.GetAccount(accountId);
        if (acc == null)
        {
            return NotFound();
        }
        acc.Status = account.Status;
        accountService.UpdateAccount(acc);
        return Ok();
    }
    [HttpDelete("{acid}")]
    public IActionResult CloseAccount(string acid)
    {
        Guid.TryParse(acid, out var accountId);
        if (accountId == Guid.Empty)
        {
            return BadRequest();
        }
        var account = accountService.GetAccount(accountId);
        if (account == null)
        {
            return BadRequest();
        }
        accountService.CloseAccount(account);
        return Ok();
    }
    [HttpPost("{acid}/Deposit/{amount}")]
    public IActionResult Deposit(string acid, string amount)
    {
        Guid.TryParse(acid, out var accountId);
        if (accountId == Guid.Empty)
        {
            return BadRequest();
        }
        var account = accountService.GetAccount(accountId);
        if (account == null)
        {
            return NotFound();
        }
        var transactionId = accountService.Deposit(account, Decimal.Parse(amount));
        return Ok(transactionId);
    }
    [HttpPost("{acid}/Withdraw/{amount}")]
    public IActionResult Withdraw(string acid, string amount)
    {
        Guid.TryParse(acid, out var accountId);
        if (accountId == Guid.Empty)
        {
            return BadRequest();
        }
        var account = accountService.GetAccount(accountId);
        if (account == null)
        {
            return NotFound();
        }
        try
        {
            var transactionId = accountService.Withdraw(account, Decimal.Parse(amount));
            return Ok(transactionId);
        }
        catch (InsufficientBalanceException)
        {
            return BadRequest();
        }
    }
    [HttpPost("{sid}/Transfer/{rid}/{amount}")]
    public IActionResult Transfer(string sid, string rid, string amount)
    {
        if (Guid.TryParse(sid, out var senderAccountId) == false || Guid.TryParse(rid, out var recipientAccountId) == false)
        {
            return BadRequest();
        }
        var senderAccount = accountService.GetAccount(senderAccountId);
        var recipientAccount = accountService.GetAccount(recipientAccountId);
        if (senderAccount == null || recipientAccount == null)
        {
            return NotFound();
        }
        try
        {
            var transactionId = accountService.Transfer(senderAccount, recipientAccount, Decimal.Parse(amount));
            return Ok(transactionId);
        }
        catch (InsufficientBalanceException)
        {
            return BadRequest();
        }
    }
}
