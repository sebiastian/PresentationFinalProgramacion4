using Contract.Location.Request;
using Contract.Location.Response;
using Contract.User.Request;
using Contract.User.Response;

namespace Application.Service;

public interface ILocationService
{
    List<LocationResponse> GetAllLocation();
    LocationResponse GetLocation(string id);

    bool UpdateLocation(int id, UpdateLocationRequest request);
    bool DeleteLocation(int id);

    bool CreateLocation(CreateLocationRequest request);
}