using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService transactionService;
    private readonly IAccountService accountService;

    public TransactionsController(ITransactionService transactionService, IAccountService accountService)
    {
        this.transactionService = transactionService;
        this.accountService = accountService;
    }
    [HttpGet("{id}")]
    public IActionResult GetTransactions(string id)
    {
        if (Guid.TryParse(id, out var accountId) == false)
        {
            return BadRequest();
        }
        var account = accountService.GetAccount(accountId);
#pragma warning disable CS8604 // Possible null reference argument.
        var transactions = transactionService.GetTransactions(account);
#pragma warning restore CS8604 // Possible null reference argument.
        return Ok(transactions);
    }
    [HttpGet("{id}")]
    public IActionResult GetTransaction(string id)
    {
        if (Guid.TryParse(id, out var transactionId) == false)
        {
            return BadRequest();
        }
        var transaction = transactionService.GetTransaction(transactionId);
        if (transaction == null)
        {
            return NotFound();
        }
        return Ok(transaction);
    }
    [HttpPost("{id}")]
    public IActionResult AddTransaction(string id, [FromBody] Transaction transaction)
    {
        if (Guid.TryParse(id, out var accountId) == false)
        {
            return BadRequest();
        }
        var account = accountService.GetAccount(accountId);
        if (account == null)
        {
            return NotFound();
        }
        var ret = transactionService.AddTransaction(account, transaction);
        return Ok(ret);
    }
    [HttpPost("{id}")]
    public IActionResult UpdateTransaction(string id, [FromBody] Transaction transaction)
    {
        if (Guid.TryParse(id, out var transactionId) == false)
        {
            return BadRequest();
        }
        var txn = transactionService.GetTransaction(transactionId);
        if (txn == null)
        {
            return NotFound();
        }
        txn.Amount = transaction.Amount;
        transactionService.UpdateTransaction(txn);
        return Ok();
    }
}
