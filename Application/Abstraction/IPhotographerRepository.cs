using Domain.Entity;

namespace Application.Abstraction;

public interface IPhotographerRepository
{
    Photographer GetPhotographerById(int id);
}