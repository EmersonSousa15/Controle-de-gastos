using Microsoft.AspNetCore.Mvc;
using Models;
using Repository.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository repository;

    public CategoryController(ICategoryRepository _repository)
    {
        repository = _repository;
    }

    [HttpGet]
    public IActionResult Get() => Ok(repository.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var category = repository.GetById(id);
        return category is null ? NotFound() : Ok(category);
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        repository.Add(category);
        return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Category category)
    {
        if (id != category.CategoryId) return BadRequest();
        repository.Update(category);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        repository.Delete(id);
        return NoContent();
    }
}
