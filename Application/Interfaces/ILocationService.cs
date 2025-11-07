using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Location.Request;
using Contract.Location.Response;

namespace Application.Interfaces;

public interface ILocationService
{
    Task<List<LocationResponse>> GetAllLocation();
    LocationResponse GetLocation(string id);

    bool UpdateLocation(int id, UpdateLocationRequest request);
    bool DeleteLocation(int id);

    bool CreateLocation(CreateLocationRequest request);
}