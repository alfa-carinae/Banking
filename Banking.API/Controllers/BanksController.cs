using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Banking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IBankService bankService;

        public BanksController(IBankService bankService)
        {
            this.bankService = bankService;
        }

        // GET: api/<BanksController>
        [HttpGet]
        public IEnumerable<Bank> Get()
        {
            return bankService.GetBanks();
        }

        // GET api/<BanksController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
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

        // POST api/<BanksController>
        [HttpPost]
        public void Post([FromBody] Bank bank)
        {
            bankService.AddBank(bank);
        }

        // DELETE api/<BanksController>/5
        [HttpDelete("{id}")]
        public void Delete(Bank bank)
        {
            //
        }
    }
}
