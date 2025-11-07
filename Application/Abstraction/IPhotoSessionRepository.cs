using Domain.Entity;
using System.Collections.Generic;

namespace Application.Abstraction;

public interface IPhotoSessionRepository : IRepositoryBase<PhotoSession>
{
    // Métodos específicos que mantienen los nombres originales y lógica de relaciones
    PhotoSession GetById(int id);
    List<PhotoSession> GetAll();
    PhotoSession Create(PhotoSession newSession);
    bool Update(PhotoSession session);
    PhotoSession Delete(int id);
}