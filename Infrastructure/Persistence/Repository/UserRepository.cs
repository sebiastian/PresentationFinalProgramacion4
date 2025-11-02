using Application.Abstraction;
using Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistence.Repository;

public class UserRepository : IUserRepository
{
    // Simulación de base de datos en memoria
    private static readonly List<User> _users = new List<User>
    {
        new User { Id = 1, Name = "Juan", Email = "juan@example.com", Phone = "600123456" },
        new User { Id = 2, Name = "Ana", Email = "ana@example.com", Phone = "600654321" },
        new User { Id = 3, Name = "Luis", Email = "luis@example.com", Phone = "600111222" }
    };

    public List<User> GetAllClients()
    {
        return _users;
    }

    public User GetUserById(int id)
    {
        // Simula una búsqueda en base de datos
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
            return user;

        // Si no existe, devolvemos un usuario "vacío" o lanzar excepción según convenga.
        return new User
        {
            Id = id,
            Name = "Unknown",
            Email = "unknown@example.com",
            Phone = string.Empty
        };
    }
    public bool DeleteClient(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _users.Remove(user);
            return true;
        }
        return false;
    }

    public bool CreateUser(User user)
    {
        // Simula la creación de un usuario en base de datos
        user.Id = _users.Max(u => u.Id) + 1; // Asigna un nuevo Id
        _users.Add(user);
        return true;
    }

    

    public bool UpdateUser(User user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            return true;
        }
        return false;

    }
}