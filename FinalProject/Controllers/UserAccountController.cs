using FinalProject.Dto;
using FinalProject.Models;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class UserAccountController : ControllerBase 
{
    private readonly UserAccountRepository _userAccountRepository;
    private readonly CartRepository _cartRepository;
    private readonly OrderRepository _orderRepository;

    public UserAccountController(UserAccountRepository userAccountRepository, CartRepository cartRepository, OrderRepository orderRepository)
    {
        _userAccountRepository = userAccountRepository;
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
    }
    
    /// <summary>
    /// Get list of users.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(await _userAccountRepository.GetAll());
    }
    
    /// <summary>
    /// Get user by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> One(int id)
    {
        return Ok(await _userAccountRepository.Get(id));
    }
    
    /// <summary>
    /// Create single user.
    /// </summary>
    /// <param name="userCreateDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateDto userCreateDto)
    {
        return Ok(await this.CreateOne(userCreateDto));
    }
    
    /// <summary>
    /// Create multiple users at once.
    /// </summary>
    /// <param name="userCreateDtos"></param>
    /// <returns></returns>
    [HttpPost("Many")]
    public async Task<IActionResult> CreateMany([FromBody] UserCreateDto[] userCreateDtos)
    {
        var createdItems = new List<UserAccount>();
        
        foreach (var userCreateDto in userCreateDtos)
        {
            createdItems.Add(await this.CreateOne(userCreateDto));
        }

        return Ok(createdItems);
    }

    private async Task<UserAccount> CreateOne(UserCreateDto userCreateDto)
    {
        var user = new UserAccount()
        {
            Name = userCreateDto.Name,
            Address = userCreateDto.Address,
            Age = userCreateDto.Age,
            Gender = userCreateDto.Gender,
            UserName = userCreateDto.UserName,
            Password = userCreateDto.Password,
            ContactNumber = userCreateDto.ContactNumber,
            UserTypeId = userCreateDto.UserTypeId,
        };
        
        return await _userAccountRepository.Add(user);
    }
    
    /// <summary>
    /// Update user.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userCreateDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto userCreateDto)
    {
        var userAccount = await _userAccountRepository.Get(id);

        if (userAccount == null)
        {
            return NotFound("No user account with provided id");
        }

        if (userCreateDto.Name != null)
        {
            userAccount.Name = userCreateDto.Name;
        }

        if (userCreateDto.Address != null)
        {
            userAccount.Address = userCreateDto.Address;
        }
        
        if (userCreateDto.Age != null)
        {
            userAccount.Age = (int)userCreateDto.Age;
        }
        
        if (userCreateDto.Gender != null)
        {
            userAccount.Gender = userCreateDto.Gender;
        }
        
        if (userCreateDto.UserName != null)
        {
            userAccount.UserName = userCreateDto.UserName;
        }

        if (userCreateDto.Password != null)
        {
            userAccount.Password = userCreateDto.Password;
        }
        
        if (userCreateDto.ContactNumber != null)
        {
            userAccount.ContactNumber = (int)userCreateDto.ContactNumber;
        }
        
        
        if (userCreateDto.UserTypeId != null)
        {
            userAccount.UserTypeId = (int)userCreateDto.UserTypeId;
        }
        
        return Ok(await _userAccountRepository.Add(userAccount));
    }
    
    /// <summary>
    /// Delete user by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        return Ok(await _userAccountRepository.Delete(id));
    }

    /// <summary>
    /// Get user's current cart.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/CurrentCart")]
    public ActionResult<ICollection<Cart>> CurrentCart(int id)
    {
        return Ok(_cartRepository.GetCurrentCart());
    }
    
    /// <summary>
    /// Get user orders based on time range (start and end dates)
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    [HttpGet("Orders")]
    public async Task<ActionResult<ICollection<Order>>> GetUserOrders(int userId, DateTime? startDate, DateTime? endDate)
    {
        return Ok(await _orderRepository.GetAllByUserId(userId, startDate, endDate));
    }
}