using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CurrenciesController : ControllerBase
{
    private readonly ICurrencyService currencyService;

    public CurrenciesController(ICurrencyService currencyService)
    {
        this.currencyService = currencyService;
    }
    [HttpGet]
    public IActionResult GetCurrencies()
    {
        var  currencies = currencyService.GetCurrencies();
        if (currencies == null)
        {
            return NotFound();
        }
        return Ok(currencies);
    }
    [HttpGet("{code}")]
    public IActionResult GetCurrency(string code)
    {
        var currency = currencyService.GetCurrency(code);
        if (currency == null)
        {
            return BadRequest();
        }
        return Ok(currency);
    }
    [HttpPost]
    public IActionResult AddCurrency([FromBody] Currency currency)
    {
        currencyService.AddCurrency(currency);
        return Ok();
    }
    [HttpPost("{code}")]
    public IActionResult UpdateCurrency(string code, [FromBody] Currency currency)
    {
        var cur = currencyService.GetCurrency(code);
        if (cur == null)
        {
            return BadRequest();
        }
        cur.ExchangeRate = currency.ExchangeRate;
        currencyService.UpdateCurrency(cur);
        return Ok();
    }
}
