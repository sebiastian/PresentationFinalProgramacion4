using Application.Abstraction;
using Domain.Entity;

namespace Infrastructure.Persistence.Repository;

public class PhotographerRepository : IPhotographerRepository
{
    // Ejemplo en memoria; sustituir por acceso real (EF Core, Dapper...) en producción.
    public Photographer GetPhotographerById(int id)
    {
        if (id == 1)
        {
            return new Photographer
            {
                Id = 1,
                Name = "Laura",
                Email = "laura@example.com",
                Phone = "600987654",
                PasswordHash = "hashed_password_example",
                Rol = "Admin"
            };
        }

        return new Photographer
        {
            Id = id,
            Name = "Desconocido",
            Email = "unknown@example.com",
            Phone = string.Empty,
            PasswordHash = string.Empty,
            Rol = string.Empty
        };
    }
}