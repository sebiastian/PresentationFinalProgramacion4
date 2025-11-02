using Application.Abstraction;
using Contract.PhotoSession.Response;

namespace Application.Service;

public class PhotoSessionService : IPhotoSessionService
{
    private readonly IPhotoSessionRepository _repo;

    public PhotoSessionService(IPhotoSessionRepository repo) => _repo = repo;

    public PhotoSessionResponse GetById(int id)
    {
        var s = _repo.GetById(id);
        if (s == null) return null;

        return new PhotoSessionResponse
        {
            Id = s.Id,
            Date = s.Date,
            SessionType = s.SessionType.ToString(),
            Status = s.Status.ToString(),
            PhotoCount = s.PhotoCount,
            DeliveryFormat = s.DeliveryFormat,
            LocationId = s.LocationId,
            ClientId = s.ClientId,
            PhotographerId = s.PhotographerId
        };
    }
}