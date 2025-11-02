using Application.Abstraction;
using Contract.Photographer.Response;

namespace Application.Service;

public class PhotographerService : IPhotographerService
{
    private readonly IPhotographerRepository _photographerRepository;

    public PhotographerService(IPhotographerRepository photographerRepository)
    {
        _photographerRepository = photographerRepository;
    }

    public PhotographerResponse GetPhotographerById(int id)
    {
        var p = _photographerRepository.GetPhotographerById(id);

        // No exponer PasswordHash; mapear Nombre->Name y Rol->Role
        return new PhotographerResponse
        {
            Name = p.Name,
            Email = p.Email,
            Role = p.Rol
        };
    }
}