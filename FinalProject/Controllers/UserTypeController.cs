using FinalProject.Dto;
using FinalProject.Models;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class UserTypeController : ControllerBase 
{
    private readonly UserTypeRepository _userTypeRepository;

    public UserTypeController(UserTypeRepository userTypeRepository)
    {
        _userTypeRepository = userTypeRepository;
    }
    
    /// <summary>
    /// Get list of user types.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(await _userTypeRepository.GetAll());
    }
    
    /// <summary>
    /// Get user type by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> One(int id)
    {
        return Ok(await _userTypeRepository.Get(id));
    }
    
    /// <summary>
    /// Create single user type.
    /// </summary>
    /// <param name="userTypeDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserTypeCreateDto userTypeDto)
    {
        var userType = new UserType()
        {
            TypeName = userTypeDto.TypeName,
            Description = userTypeDto.Description,
        };
        
        return Ok(await _userTypeRepository.Add(userType));
    }
    
    /// <summary>
    /// Create multiple user types.
    /// </summary>
    /// <param name="userTypeDtos"></param>
    /// <returns></returns>
    [HttpPost("Many")]
    public async Task<IActionResult> CreateMany([FromBody] UserTypeCreateDto[] userTypeDtos)
    {
        var createdItems = new List<UserType>();
        
        foreach (var userTypeDto in userTypeDtos)
        {
            createdItems.Add(await this.CreateOne(userTypeDto));
        }

        return Ok(createdItems);
    }

    private async Task<UserType> CreateOne(UserTypeCreateDto userTypeDto)
    {
        var userType = new UserType()
        {
            TypeName = userTypeDto.TypeName,
            Description = userTypeDto.Description,
        };
        
        return await _userTypeRepository.Add(userType);
    }
    
    /// <summary>
    /// Update user type.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userTypeDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserTypeUpdateDto userTypeDto)
    {
        var userType = await _userTypeRepository.Get(id);

        if (userType == null)
        {
            return NotFound("No user type with provided id");
        }

        if (userTypeDto.TypeName != null)
        {
            userType.TypeName = userTypeDto.TypeName;
        }

        if (userTypeDto.Description != null)
        {
            userType.Description = userTypeDto.Description;
        }

        return Ok(await _userTypeRepository.Add(userType));
    }
    
    /// <summary>
    /// Delete user type by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        return Ok(await _userTypeRepository.Delete(id));
    }
}