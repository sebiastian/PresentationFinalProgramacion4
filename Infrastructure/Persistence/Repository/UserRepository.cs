using Domain.Abstraction;
using Domain.Entity;

namespace Infrastructure.Persistence.Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(UserManagerDbContext context) : base(context)
    {
       
    }
}