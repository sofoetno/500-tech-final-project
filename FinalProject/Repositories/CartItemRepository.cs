using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories;

public class CartItemRepository : BaseRepository<CartItem, EcommerceDbContext>
{
    public CartItemRepository(EcommerceDbContext dbContext) : base(dbContext)
    {
    }
    
    public CartItem? GetCartItemByCartIdAndProductId(int cartId, int productId)
    {
        return _dbContext.CartItems
            .FirstOrDefault(c => c.CartId == cartId && c.ProductId == productId );
    }
}