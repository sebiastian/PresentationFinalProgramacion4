using Domain.Entity;
using System.Collections.Generic;
using System.Threading;

namespace Application.Abstraction;

public interface IUserRepository
{
    List<User> GetAllClients();
    User GetUserById(int id);
    bool DeleteClient(int id);
    bool CreateUser(User user);
    bool UpdateUser(User user);
        
        }
