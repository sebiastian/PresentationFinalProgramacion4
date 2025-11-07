using Domain.Entity;
using System.Collections.Generic;

namespace Application.Abstraction;

public interface IUserRepository : IRepositoryBase<User>
{
    // Métodos específicos de User (mantener compatibilidad con el servicio)
    List<User> GetAllClients();
    User GetUserById(int id);
    bool DeleteClient(int id);
    bool CreateUser(User user);
    bool UpdateUser(User user);
}
