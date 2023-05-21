using FinalProject.Dto;
using FinalProject.Models;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly PaymentRepository _paymentRepository;

    public PaymentController(PaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }
    
    /// <summary>
    /// Get list of payments.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(await _paymentRepository.GetAll());
    }

    /// <summary>
    /// Get payment by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> One(int id)
    {
        return Ok(await _paymentRepository.Get(id));
    }

    /// <summary>
    /// Create single payment.
    /// </summary>
    /// <param name="paymentDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PaymentCreateDto paymentDto)
    {
        return Ok(await this.CreateOne(paymentDto));
    }
    
    /// <summary>
    /// Create multiple payments at once.
    /// </summary>
    /// <param name="paymentDtos"></param>
    /// <returns></returns>
    [HttpPost("Many")]
    public async Task<IActionResult> CreateMany([FromBody] PaymentCreateDto[] paymentDtos)
    {
        var createdItems = new List<Payment>();
        
        foreach (var paymentDto in paymentDtos)
        {
            createdItems.Add(await this.CreateOne(paymentDto));
        }

        return Ok(createdItems);
    }

    private async Task<Payment> CreateOne(PaymentCreateDto paymentDto)
    {
        var payment = new Payment()
        {
            Amount = paymentDto.Amount,
            UserAccountId = paymentDto.UserAccountId,
            OrderId = paymentDto.OrderId,
            Date = new DateTime(),
        };

        return await _paymentRepository.Add(payment);
    }

    /// <summary>
    /// Update payment.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="paymentDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] PaymentUpdateDto paymentDto)
    {
        var payment = await _paymentRepository.Get(id);

        if (payment == null)
        {
            return NotFound("No payment with provided id");
        }

        if (paymentDto.OrderId != null)
        {
            payment.OrderId = (int)paymentDto.OrderId;
        }

        if (paymentDto.Amount != null)
        {
            payment.Amount = (int)paymentDto.Amount;
        }
        
        payment.Date = new DateTime();
        
        return Ok(await _paymentRepository.Update(payment));
    }
    
    /// <summary>
    /// Delete payment by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        return Ok(await _paymentRepository.Delete(id));
    }
}