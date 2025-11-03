using Domain.Entity;
using System.Collections.Generic;

namespace Application.Abstraction;

public interface IPhotoSessionRepository
{
    PhotoSession GetById(int id);
    List<PhotoSession> GetAll();
    PhotoSession Create(PhotoSession newSession);
    bool Update(PhotoSession session);

    PhotoSession Delete(int id);
}