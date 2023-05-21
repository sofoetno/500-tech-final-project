using FinalProject.Dto;
using FinalProject.Models;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderRepository _orderRepository;

    public OrderController(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    /// <summary>
    /// Get list of orders.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult List()
    {
        return Ok(_orderRepository.GetAll());
    }

    /// <summary>
    /// Get order by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult One(int id)
    {
        return Ok(_orderRepository.Get(id));
    }
    
    /// <summary>
    /// Create single order.
    /// </summary>
    /// <param name="orderDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderCreateOrUpdateDto orderDto)
    {
        return Ok(await this.CreateOne(orderDto));
    }
    
    /// <summary>
    /// Create multiple orders at once.
    /// </summary>
    /// <param name="orderDtos"></param>
    /// <returns></returns>
    [HttpPost("Many")]
    public async Task<IActionResult> CreateMany([FromBody] OrderCreateOrUpdateDto[] orderDtos)
    {
        var createdItems = new List<Order>();
        
        foreach (var orderDto in orderDtos)
        {
            createdItems.Add(await this.CreateOne(orderDto));
        }

        return Ok(createdItems);
    }

    private async Task<Order> CreateOne(OrderCreateOrUpdateDto orderDto)
    {
        var order = new Order()
        {
            CartId = orderDto.CartId,
            Date = new DateTime(),
        };

        return await _orderRepository.Add(order);
    }

    /// <summary>
    /// Update order.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="orderOrUpdateDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] OrderCreateOrUpdateDto orderOrUpdateDto)
    {
        var order = await _orderRepository.Get(id);

        if (order == null)
        {
            return NotFound("No order with provided id");
        }

        order.CartId = orderOrUpdateDto.CartId;
        order.Date = new DateTime();

        return Ok(await _orderRepository.Add(order));
    }

    /// <summary>
    /// Delete order by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        return Ok(await _orderRepository.Delete(id));
    }
    
    /// <summary>
    /// Get today's orders by user id.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("Today")]
    public async Task<IActionResult> Recent(int userId)
    {
        return Ok(await _orderRepository.GetRecentByUserId(userId));
    }
}