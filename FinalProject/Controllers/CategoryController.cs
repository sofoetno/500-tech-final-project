using FinalProject.Dto;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class CategoryController : ControllerBase
{
    private readonly CategoryRepository _categoryRepository;
    
    public CategoryController(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    /// <summary>
    /// Get list of categories.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(await _categoryRepository.GetAll());
    }
    
    /// <summary>
    /// Get category by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> One(int id)
    {
        return Ok(await _categoryRepository.Get(id));
    }
    
    /// <summary>
    /// Create single category.
    /// </summary>
    /// <param name="categoryDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreateDto categoryDto)
    {
        return Ok(await this.CreateOne(categoryDto));
    }

    /// <summary>
    /// Create multiple categories at once.
    /// </summary>
    /// <param name="categoryDtos"></param>
    /// <returns></returns>
    [HttpPost("Many")]
    public async Task<IActionResult> CreateMany([FromBody] CategoryCreateDto[] categoryDtos)
    {
        var createdItems = new List<Category>();
        
        foreach (var categoryDto in categoryDtos)
        {
            createdItems.Add(await this.CreateOne(categoryDto));
        }

        return Ok(createdItems);
    }

    private async Task<Category> CreateOne(CategoryCreateDto categoryDto)
    {
        var category = new Category()
        {
            CategoryName = categoryDto.CategoryName,
            Description = categoryDto.Description
        };

        return await _categoryRepository.Add(category);
    }

    /// <summary>
    /// Update category.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="categoryDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] CategoryUpdateDto categoryDto)
    {
        var category = await _categoryRepository.Get(id);

        if (category == null)
        {
            return NotFound("No category with provided id");
        }

        if (categoryDto.Description != null)
        {
            category.Description = categoryDto.Description;
        }

        if (categoryDto.CategoryName != null)
        {
            category.CategoryName = categoryDto.CategoryName;
        }

        return Ok(await _categoryRepository.Update(category));
    }
    
    /// <summary>
    /// Deletes category by id.
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        return Ok(await _categoryRepository.Delete(id));
    }
}