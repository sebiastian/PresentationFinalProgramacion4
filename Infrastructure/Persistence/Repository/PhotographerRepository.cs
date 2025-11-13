using Domain.Abstraction;
using Domain.Entity;

namespace Infrastructure.Persistence.Repository;

public class PhotographerRepository : RepositoryBase<Photographer>, IPhotographerRepository
{
    public PhotographerRepository(UserManagerDbContext context) : base(context)
    {
    }

    public bool UpdatePhotographer(int id, Photographer photographer)
    {
        var existing = GetById(id);
        if (existing == null) return false;

        // Actualizar campos específicos
        existing.Name = photographer.Name;
        existing.Email = photographer.Email;
        existing.Phone = photographer.Phone;
        if (!string.IsNullOrEmpty(photographer.PasswordHash))
        {
            existing.PasswordHash = photographer.PasswordHash;
        }
        existing.Rol = photographer.Rol;
        
        return Update(existing);
    }
}