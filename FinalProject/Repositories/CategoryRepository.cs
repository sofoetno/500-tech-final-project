namespace FinalProject.Repositories;

public class CategoryRepository : BaseRepository<Category, EcommerceDbContext>
{
    public CategoryRepository(EcommerceDbContext dbContext) : base(dbContext)
    {
        
    }
}