using Domain.Entity;
using System.Collections.Generic;

namespace Domain.Abstraction;

public interface IPhotoSessionRepository : IRepositoryBase<PhotoSession>
{
    PhotoSession GetById(int id);
    List<PhotoSession> GetAll();
    PhotoSession Create(PhotoSession newSession);
    bool Update(PhotoSession session);
    PhotoSession Delete(int id);
}