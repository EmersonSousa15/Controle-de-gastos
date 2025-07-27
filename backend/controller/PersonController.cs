using Microsoft.AspNetCore.Mvc;
using Models;
using Repository.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonRepository repository;
    private readonly ITransactionRepository transactionRepository;

    public PersonController(IPersonRepository _repository, ITransactionRepository _transactionRepository)
    {
        repository = _repository;
        transactionRepository = _transactionRepository;
    }

    [HttpGet]
    public IActionResult Get() => Ok(repository.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var person = repository.GetById(id);
        return person is null ? NotFound() : Ok(person);
    }

    [HttpPost]
    public IActionResult Create(Person person)
    {
        repository.Add(person);
        return CreatedAtAction(nameof(GetById), new { id = person.PersonId }, person);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Person person)
    {
        if (id != person.PersonId) return BadRequest();
        repository.Update(person);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        transactionRepository.DeleteByPersonId(id);
        repository.Delete(id);
        return NoContent();
    }
}
