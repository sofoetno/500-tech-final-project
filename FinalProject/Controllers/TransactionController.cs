using FinalProject.Dto;
using FinalProject.Models;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly TransactionRepository _transactionRepository;

    public TransactionController(TransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    /// <summary>
    /// Get list of transactions.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> List()
    {
        return Ok(await _transactionRepository.GetAll());
    }

    /// <summary>
    /// Get transaction by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> One(int id)
    {
        return Ok(await _transactionRepository.Get(id));
    }

    /// <summary>
    /// Create single transaction.
    /// </summary>
    /// <param name="transactionDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TransactionCreateDto transactionDto)
    {
        return Ok(await this.CreateOne(transactionDto));
    }
    
    /// <summary>
    /// Create multiple transactions at once.
    /// </summary>
    /// <param name="transactionDtos"></param>
    /// <returns></returns>
    [HttpPost("Many")]
    public async Task<IActionResult> CreateMany([FromBody] TransactionCreateDto[] transactionDtos)
    {
        var createdItems = new List<Transaction>();
        
        foreach (var transactionDto in transactionDtos)
        {
            createdItems.Add(await this.CreateOne(transactionDto));
        }

        return Ok(createdItems);
    }

    private async Task<Transaction> CreateOne(TransactionCreateDto transactionDto)
    {
        var transaction = new Transaction()
        {
            UserAccountId = transactionDto.UserAccountId,
            TransactionType = transactionDto.TransactionType,
            Description = transactionDto.Description,
            Date = new DateTime()
        };

        return await _transactionRepository.Add(transaction);
    }

    /// <summary>
    /// Update transaction.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="transactionDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] TransactionUpdateDto transactionDto)
    {
        var transaction = await _transactionRepository.Get(id);

        if (transaction == null)
        {
            return NotFound("No transaction with provided id");
        }

        if (transactionDto.UserAccountId != null)
        {
            transaction.UserAccountId = (int)transactionDto.UserAccountId;
        }

        if (transactionDto.TransactionType != null)
        {
            transaction.TransactionType = transactionDto.TransactionType;
        }

        if (transactionDto.Description != null)
        {
            transaction.Description = transactionDto.Description;
        }
        
        transaction.Date = new DateTime();
        
        return Ok(await _transactionRepository.Update(transaction));
    }
    
    /// <summary>
    /// Delete transaction by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        return Ok(await _transactionRepository.Delete(id));
    }
}