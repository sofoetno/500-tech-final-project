using FinalProject.Models;

namespace FinalProject.Repositories;

public class DeliveryRepository : BaseRepository<Delivery, EcommerceDbContext>
{
    public DeliveryRepository(EcommerceDbContext dbContext) : base(dbContext)
    {
    }
}