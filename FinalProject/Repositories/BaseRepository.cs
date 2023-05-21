using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories;

public abstract class BaseRepository<TEntity, TDbContext> : IRepository<TEntity> 
    where TEntity : class
    where TDbContext : DbContext
{
    protected readonly TDbContext _dbContext;
    
    protected BaseRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<TEntity>> GetAll()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> Get(int id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> Delete(int id)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            return null;
        }

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}