using Contract.Photographer.Response;
using Contract.Photographer.Request;
using System.Collections.Generic;

namespace Application.Service;

public interface IPhotographerService
{
    PhotographerResponse GetPhotographerById(int id);
    List<PhotographerResponse> GetAllPhotographers();

    bool CreatePhotographer(CreatePhotographerRequest request);
    bool UpdatePhotographer(int id, UpdatePhotographerRequest request);

    bool DeletePhotographer(int id);
}