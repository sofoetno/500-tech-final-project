using FinalProject.Dto;
using FinalProject.Models;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase 
{
    private readonly ProductRepository _productRepository;

    public ProductController( ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    /// <summary>
    /// Get list of products.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(await _productRepository.GetAll());
    }

    /// <summary>
    /// Get product by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> One(int id)
    {
        return Ok(await _productRepository.Get(id));
    }
    
    /// <summary>
    /// Create single product.
    /// </summary>
    /// <param name="productDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto productDto)
    {
        return Ok(await this.CreateOne(productDto));
    }
    
    /// <summary>
    /// Create multiple products at once.
    /// </summary>
    /// <param name="productDtos"></param>
    /// <returns></returns>
    [HttpPost("Many")]
    public async Task<IActionResult> CreateMany([FromBody] ProductCreateDto[] productDtos)
    {
        var createdItems = new List<Product>();
        
        foreach (var productDto in productDtos)
        {
            createdItems.Add(await this.CreateOne(productDto));
        }

        return Ok(createdItems);
    }

    private async Task<Product> CreateOne(ProductCreateDto productDto)
    {
        var product = new Product()
        {
            Description = productDto.Description,
            Name = productDto.Name,
            Count = productDto.Count,
            Price = productDto.Price,
            CategoryId = productDto.CategoryId, 
        };
        
        return await _productRepository.Add(product);
    }
    
    /// <summary>
    /// Update product.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="productDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto productDto)
    {
        var product = await _productRepository.Get(id);

        if (product == null)
        {
            return NotFound("No payment with provided id");
        }

        if (productDto.Description != null)
        {
            product.Description = productDto.Description;
        }
        
        if (productDto.Name != null)
        {
            product.Name = productDto.Name;
        }

        if (productDto.Count != null)
        {
            product.Count = (int)productDto.Count;
        }

        if (productDto.Price != null)
        {
            product.Price = (int)productDto.Price;
        }

        if (productDto.CategoryId != null)
        {
            product.CategoryId = (int)productDto.CategoryId;
        }

        return Ok(await _productRepository.Add(product));
    }
    
    /// <summary>
    /// Delete product by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        return Ok(await _productRepository.Delete(id));
    }
}