using Domain.Entity;

namespace Domain.Abstraction;

public interface IPhotographerRepository : IRepositoryBase<Photographer>
{
    
    bool UpdatePhotographer(int id, Photographer photographer);
}