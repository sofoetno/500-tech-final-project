using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories;

public class OrderRepository : BaseRepository<Order, EcommerceDbContext>
{
    public OrderRepository(EcommerceDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Order>> GetAllByUserId(int userId, DateTime? startDate, DateTime? endDate)
    {
        return await _dbContext.Orders
            .Include(o => o.Cart)
            .Include(o => o.Cart.Items)
            .Include(o => o.Delivery)
            .Where(o => o.Cart.UserAccountId == userId 
                && (startDate == null || o.Date >= startDate)
                && (endDate == null || o.Date <= endDate))
            .ToListAsync();
    }

    public async Task<List<Order>> GetRecentByUserId(int userId)
    {
        var today = DateTime.Today; 
        var startDate = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0, 0);
        var endDate = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59, 999);

        return await _dbContext.Orders
            .Include(o => o.Cart)
            .Include(o => o.Cart.Items)
            .Include(o => o.Delivery)
            .Where(o => o.Cart.UserAccountId == userId && o.Date >= startDate && o.Date <= endDate)
            .ToListAsync();
    }
}