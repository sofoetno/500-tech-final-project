using FinalProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController<TEntity, TRepository, TCreateDto, TUpdateDto> : ControllerBase
    where TEntity : class
    where TCreateDto : class
    where TUpdateDto : class
    where TRepository : IRepository<TEntity>
{
    private readonly TRepository _repository;
    
    public BaseController(TRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(await _repository.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> One(int id)
    {
        return Ok(await _repository.Get(id));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        return Ok(await _repository.Delete(id));
    }
}