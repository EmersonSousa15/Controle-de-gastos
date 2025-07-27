using Microsoft.AspNetCore.Mvc;
using Models;
using Repository.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionRepository repository;

    public TransactionController(ITransactionRepository _repository)
    {
        repository = _repository;
    }

    [HttpGet]
    public IActionResult Get() => Ok(repository.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var transaction = repository.GetById(id);
        return transaction is null ? NotFound() : Ok(transaction);
    }

    [HttpGet("person/{personId}")]
    public IActionResult GetByPersonId(int personId)
    {
        var transactions = repository.GetByPersonId(personId);
        return Ok(transactions);
    }



    [HttpPost]
    public IActionResult Create(Transaction transaction)
    {
        repository.Add(transaction);
        return CreatedAtAction(nameof(GetById), new { id = transaction.TransactionId }, transaction);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Transaction transaction)
    {
        if (id != transaction.TransactionId) return BadRequest();
        repository.Update(transaction);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        repository.Delete(id);
        return NoContent();
    }
}
