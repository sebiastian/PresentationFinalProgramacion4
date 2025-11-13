using Domain.Abstraction;
using Application.Interfaces;
using Contract.Photographer.Request;
using Contract.Photographer.Response;
using Domain.Entity;
using System.Security.Cryptography;
using System.Text;

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
        // REFACTORIZACIÓN: Cambio de .GetPhotographerById() → .GetById()
        var p = _photographerRepository.GetById(id);
        if (p == null) return null;

        return new PhotographerResponse
        {
            Id = p.Id,
            Name = p.Name,
            Email = p.Email,
            Phone = p.Phone,
            Role = p.Rol
        };
    }

    public List<PhotographerResponse> GetAllPhotographers()
    {
        // REFACTORIZACIÓN: Cambio de .GetAllPhotographers() → .GetAll()
        return _photographerRepository.GetAll()
            .Select(p => new PhotographerResponse
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                Role = p.Rol
            }).ToList();
    }

    public bool CreatePhotographer(CreatePhotographerRequest request)
    {
        var photographer = new Photographer
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            PasswordHash = HashPassword(request.Password),
            Rol = request.Role
        };

        // REFACTORIZACIÓN: Cambio de .CreatePhotographer() → .Create()
        return _photographerRepository.Create(photographer);
    }

    public bool UpdatePhotographer(int id, UpdatePhotographerRequest request)
    {
        // REFACTORIZACIÓN: Cambio de .GetPhotographerById() → .GetById()
        var existing = _photographerRepository.GetById(id);
        if (existing == null) return false;

        var updated = new Photographer
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
        };

        // ✅ MANTENER: .UpdatePhotographer() porque tiene lógica específica
        return _photographerRepository.UpdatePhotographer(id, updated);
    }

    private static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashed = sha.ComputeHash(bytes);
        return Convert.ToHexString(hashed);
    }

    public bool DeletePhotographer(int id)
    {
        // REFACTORIZACIÓN: Cambio de .DeletePhotographer() → .Delete()
        return _photographerRepository.Delete(id);
    }
}