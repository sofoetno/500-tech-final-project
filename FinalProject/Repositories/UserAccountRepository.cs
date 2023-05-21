using FinalProject.Models;

namespace FinalProject.Repositories;

public class UserAccountRepository : BaseRepository<UserAccount, EcommerceDbContext>
{
    public UserAccountRepository(EcommerceDbContext dbContext) : base(dbContext)
    {
        
    }
}