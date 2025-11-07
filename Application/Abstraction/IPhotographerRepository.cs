using Domain.Entity;
using System.Collections.Generic;

namespace Application.Abstraction;

public interface IPhotographerRepository : IRepositoryBase<Photographer>
{
    // Métodos específicos para mantener compatibilidad
    Photographer GetPhotographerById(int id);
    List<Photographer> GetAllPhotographers();
    bool CreatePhotographer(Photographer photographer);
    bool UpdatePhotographer(int id, Photographer photographer);
    bool DeletePhotographer(int id);
}