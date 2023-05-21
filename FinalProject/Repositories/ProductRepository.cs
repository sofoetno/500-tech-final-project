using FinalProject.Models;

namespace FinalProject.Repositories;

public class ProductRepository : BaseRepository<Product, EcommerceDbContext>
{
    public ProductRepository(EcommerceDbContext dbContext) : base(dbContext)
    {
        
    }
}