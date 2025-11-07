using Contract.PhotoSession.Request;
using Contract.PhotoSession.Response;
using System.Collections.Generic;

namespace Application.Interfaces;

public interface IPhotoSessionService
{
    PhotoSessionResponse GetById(int id);
    List<PhotoSessionResponse> GetAll();
    PhotoSessionResponse CreatePhotoSession(CreatePhotoSessionRequest request);
    PhotoSessionResponse UpdatePhotoSession(UpdatePhotoSessionRequest request);

    PhotoSessionResponse DeletePhotoSession(int id);
}