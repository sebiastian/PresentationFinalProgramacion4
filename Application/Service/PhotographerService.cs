using Application.Abstraction;
using Application.Interfaces;
using Contract.Photographer.Request;
using Contract.Photographer.Response;
using Domain.Entity;
using System.Collections.Generic;
using System.Linq;
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
        var p = _photographerRepository.GetPhotographerById(id);
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
        return _photographerRepository.GetAllPhotographers()
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

        return _photographerRepository.CreatePhotographer(photographer);
    }

    public bool UpdatePhotographer(int id, UpdatePhotographerRequest request)
    {
        var existing = _photographerRepository.GetPhotographerById(id);
        if (existing == null) return false;

        var updated = new Photographer
        {
            // Id será usado por el repositorio para localizar el registro a actualizar
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
        };

        return _photographerRepository.UpdatePhotographer(id, updated);
    }

    private static string HashPassword(string password)
    {
        // Hash simple SHA256 (simulado para el repositorio en memoria)
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashed = sha.ComputeHash(bytes);
        return Convert.ToHexString(hashed);
    }

    public bool DeletePhotographer(int id)
    {
        return _photographerRepository.DeletePhotographer(id);

    }
}