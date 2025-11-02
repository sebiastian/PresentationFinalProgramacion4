using Domain.Entity;

namespace Application.Abstraction;

public interface IPhotoSessionRepository
{
    PhotoSession GetById(int id);
}