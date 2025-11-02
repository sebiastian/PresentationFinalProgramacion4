using Contract.Photographer.Response;

namespace Application.Service;

public interface IPhotographerService
{
    PhotographerResponse GetPhotographerById(int id);
}