using FinalProject.Models;

namespace FinalProject.Repositories;

public class PaymentRepository : BaseRepository<Payment, EcommerceDbContext>
{
    public PaymentRepository(EcommerceDbContext dbContext) : base(dbContext)
    {
    }
}