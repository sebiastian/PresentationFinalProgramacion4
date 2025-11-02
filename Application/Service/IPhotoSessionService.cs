using Contract.PhotoSession.Response;

namespace Application.Service;

public interface IPhotoSessionService
{
    PhotoSessionResponse GetById(int id);

}