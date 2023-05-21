using FinalProject.Dto;
using FinalProject.Models;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class DeliveryController : ControllerBase
{
    private readonly DeliveryRepository _deliveryRepository;

    public DeliveryController(DeliveryRepository deliveryRepository)
    {
        _deliveryRepository = deliveryRepository;
    }

    /// <summary>
    /// Get list of deliveries.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(await _deliveryRepository.GetAll());
    }

    /// <summary>
    /// Get delivery by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> One(int id)
    {
        return Ok(await _deliveryRepository.Get(id));
    }
    
    /// <summary>
    /// Create single delivery.
    /// </summary>
    /// <param name="deliveryDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DeliveryCreateOrUpdateDto deliveryDto)
    {
        return Ok(await this.CreateOne(deliveryDto));
    }
    
    /// <summary>
    /// Create multiple deliveries at once.
    /// </summary>
    /// <param name="deliveryDtos"></param>
    /// <returns></returns>
    [HttpPost("Many")]
    public async Task<IActionResult> CreateMany([FromBody] DeliveryCreateOrUpdateDto[] deliveryDtos)
    {
        var createdItems = new List<Delivery>();
        
        foreach (var deliveryDto in deliveryDtos)
        {
            createdItems.Add(await this.CreateOne(deliveryDto));
        }

        return Ok(createdItems);
    }

    private async Task<Delivery> CreateOne(DeliveryCreateOrUpdateDto deliveryDto)
    {
        var delivery = new Delivery()
        {
            OrderId = deliveryDto.OrderId
        };

        return await _deliveryRepository.Add(delivery);
    }

    /// <summary>
    /// Update delivery.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="deliveryDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] DeliveryCreateOrUpdateDto deliveryDto)
    {
        var delivery = await _deliveryRepository.Get(id);

        if (delivery == null)
        {
            return NotFound("No delivery with provided id");
        }

        delivery.OrderId = deliveryDto.OrderId;
        delivery.Date = new DateTime();

        return Ok(await _deliveryRepository.Update(delivery));
    }

    /// <summary>
    /// Delete delivery by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        return Ok(await _deliveryRepository.Delete(id));
    }
}