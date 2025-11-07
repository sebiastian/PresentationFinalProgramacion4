using Application.Abstraction;
using Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistence.Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(UserManagerDbContext context) : base(context)
    {
    }


    public List<User> GetAllClients()
    {
        return GetAll();
    }

    public User GetUserById(int id)
    {
        return GetById(id);
    }

    public bool DeleteClient(int id)
    {
        return Delete(id);
    }

    public bool CreateUser(User user)
    {
        return Create(user);
    }

    public bool UpdateUser(User user)
    {
        return Update(user);
    }
}