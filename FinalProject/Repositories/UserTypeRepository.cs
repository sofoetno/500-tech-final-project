using FinalProject.Models;

namespace FinalProject.Repositories;

public class UserTypeRepository : BaseRepository<UserType, EcommerceDbContext>
{
    public UserTypeRepository(EcommerceDbContext dbContext) : base(dbContext)
    {
        
    }
}