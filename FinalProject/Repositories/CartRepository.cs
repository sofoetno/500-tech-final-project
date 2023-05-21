using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories;

public class CartRepository : BaseRepository<Cart, EcommerceDbContext>
{
    public CartRepository(EcommerceDbContext dbContext) : base(dbContext)
    {
    }
    
    public Cart? GetCurrentCart()
    {
        return _dbContext.Carts
            .Include(c => c.Items)
            .FirstOrDefault(c => !_dbContext.Orders
            .Select(o => o.CartId)
            .Contains(c.Id)
        );
    }
}