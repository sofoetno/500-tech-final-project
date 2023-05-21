using FinalProject.Models;

namespace FinalProject.Repositories;

public class TransactionRepository : BaseRepository<Transaction, EcommerceDbContext>
{
    public TransactionRepository(EcommerceDbContext dbContext) : base(dbContext)
    {
    }
}