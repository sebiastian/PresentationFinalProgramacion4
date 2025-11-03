using Application.Abstraction;
using Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistence.Repository;

public class PhotographerRepository : IPhotographerRepository
{
    private static readonly List<Photographer> _photographers = new()
    {
        new Photographer { Id = 1, Name = "Ana Pérez", Email = "ana@foto.com", Phone = "123456789", PasswordHash = "hash1", Rol = "Principal" },
        new Photographer { Id = 2, Name = "Luis Gómez", Email = "luis@foto.com", Phone = "987654321", PasswordHash = "hash2", Rol = "Asistente" }
    };

    public Photographer GetPhotographerById(int id)
    {
        return _photographers.FirstOrDefault(p => p.Id == id);
    }

    public List<Photographer> GetAllPhotographers()
    {
        return _photographers;
    }

    public bool CreatePhotographer(Photographer photographer)
    {
        var nextId = _photographers.Any() ? _photographers.Max(p => p.Id) + 1 : 1;
        photographer.Id = nextId;
        _photographers.Add(photographer);
        return true;
    }

    public bool UpdatePhotographer(int id, Photographer photographer)
    {
        var existing = _photographers.FirstOrDefault(p => p.Id == id);
        if (existing == null) return false;

        // Actualizar campos (respetar relaciones en el futuro)
        existing.Name = photographer.Name;
        existing.Email = photographer.Email;
        existing.Phone = photographer.Phone;
        if (!string.IsNullOrEmpty(photographer.PasswordHash))
        {
            existing.PasswordHash = photographer.PasswordHash;
        }
        existing.Rol = photographer.Rol;
        return true;
    }

    public bool DeletePhotographer(int id)
    {
        var photographer = _photographers.FirstOrDefault(p => p.Id == id);
        if (photographer == null) return false;
        _photographers.Remove(photographer);
        return true;
    }   
}