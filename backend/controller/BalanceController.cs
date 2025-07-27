using Microsoft.AspNetCore.Mvc;
using Models;
using Repository.Interfaces;
using Service.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class BalanceController : ControllerBase
{
    private readonly IBalanceService balanceService;

    public BalanceController(IBalanceService _balanceService)
    {
        balanceService = _balanceService;
    }

    [HttpGet]
    public IActionResult GetBalances()
    {
        var balances = balanceService.CalculateAll();
        return Ok(balances);
    }

    [HttpGet("total")]
    public IActionResult GetBalanceAllTransactions()
    {
        var balances = balanceService.CalculateAllTransactions();
        return Ok(balances);
    }


    [HttpGet("{personId}")]
    public IActionResult GetBalance(int personId)
    {
        var balance = balanceService.Calculate(personId);
        return balance is null ? NotFound() : Ok(balance);
    }

    [HttpPost]
    public IActionResult AddBalance([FromBody] Balance balance)
    {
        try
        {
            balanceService.AddBalance(balance);
            return CreatedAtAction(nameof(GetBalance), new { personId = balance.PersonId }, balance);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("{personId}")]
    public IActionResult UpdateBalance(int personId, [FromBody] Balance balance)
    {
        if (personId != balance.PersonId)
            return BadRequest("ID da pessoa não corresponde.");

        try
        {
            balanceService.UpdateBalance(balance);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpDelete("{personId}")]
    public IActionResult DeleteBalance(int personId)
    {
        try
        {
            balanceService.DeleteBalance(personId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

}
