using Application.Abstraction;
using Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistence.Repository;

public class PhotographerRepository : RepositoryBase<Photographer>, IPhotographerRepository
{
    public PhotographerRepository(UserManagerDbContext context) : base(context)
    {
    }

    // Métodos específicos que mantienen compatibilidad con el servicio
    public Photographer GetPhotographerById(int id)
    {
        return GetById(id);
    }

    public List<Photographer> GetAllPhotographers()
    {
        return GetAll();
    }

    public bool CreatePhotographer(Photographer photographer)
    {
        return Create(photographer);
    }

    public bool UpdatePhotographer(int id, Photographer photographer)
    {
        var existing = GetById(id);
        if (existing == null) return false;

        // Actualizar campos
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

    public bool DeletePhotographer(int id)
    {
        return Delete(id);
    }
}