using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BanksController : ControllerBase
{
    private readonly IBankService bankService;

    public BanksController(IBankService bankService)
    {
        this.bankService = bankService;
    }

    [HttpGet]
    public IActionResult GetBanks()
    {
        return Ok(bankService.GetBanks());
    }

    [HttpGet("{id}")]
    public IActionResult GetBank(string id)
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
        return Ok(bank);
    }

    [HttpPost]
    public void AddBank([FromBody] Bank bank)
    {
        bankService.AddBank(bank);
    }

    [HttpPost("{id}")]
    public IActionResult UpdateBank(string id, [FromBody] Bank bank)
    {
        Guid.TryParse(id, out var bankId);
        if (bankId == Guid.Empty)
        {
            return BadRequest();
        }
        var bnk = bankService.GetBank(bankId);
        if (bnk == null)
        {
            return BadRequest();
        }
        bnk.Name = bank.Name;
        bnk.BranchName = bank.BranchName;
        bnk.Address = bank.Address;
        bnk.City = bank.City;
        bnk.State = bank.State;
        bnk.Phone = bank.Phone;
        bankService.UpdateBank(bnk);
        return Ok();
    }

    [HttpDelete("{id}")]
    public void Delete(Bank bank)
    {
        //
    }
}
