using Application.Abstraction;
using Application.Interfaces;
using Contract.Location.Response;
using Contract.Photographer.Response;
using Contract.PhotoSession.Request;
using Contract.PhotoSession.Response;
using Contract.User.Response;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Service;

public class PhotoSessionService : IPhotoSessionService
{
    private readonly IPhotoSessionRepository _repo;

    public PhotoSessionService(IPhotoSessionRepository repo) => _repo = repo;

    public PhotoSessionResponse GetById(int id)
    {
        var s = _repo.GetById(id);
        if (s == null) return null;
        return Map(s);
    }

    public List<PhotoSessionResponse> GetAll()
    {
        return _repo.GetAll().Select(Map).ToList();
    }

    public PhotoSessionResponse CreatePhotoSession(CreatePhotoSessionRequest request)
    {
        if (request.Date < DateTime.UtcNow)
        {
            // opción: validar / lanzar excepción; aquí lo permitimos
        }

        var newSession = new PhotoSession
        {
            Date = request.Date,
            SessionType = request.SessionType,
            LocationId = request.LocationId,
            ClientId = request.ClientId,
            PhotographerId = request.PhotographerId,
            DeliveryFormat = request.DeliveryFormat,
            Status = SessionStatus.Pending,
            PhotoCount = 0
        };

        var created = _repo.Create(newSession);
        return created == null ? null : Map(created);
    }

    public PhotoSessionResponse UpdatePhotoSession(UpdatePhotoSessionRequest request)
    {
        // Mapear a entidad de dominio
        var session = new PhotoSession
        {
            Id = request.Id,
            Date = request.Date,
            SessionType = request.SessionType,
            Status = request.Status,
            PhotoCount = request.PhotoCount,
            DeliveryFormat = request.DeliveryFormat,
            LocationId = request.LocationId,
            ClientId = request.ClientId,
            PhotographerId = request.PhotographerId
        };

        var ok = _repo.Update(session);
        return ok ? Map(_repo.GetById(session.Id)) : null;
    }

    private static PhotoSessionResponse Map(Domain.Entity.PhotoSession s)
    {
        var resp = new PhotoSessionResponse
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

        if (s.Location != null)
        {
            resp.Location = new LocationResponse
            {
                Id = s.Location.Id,
                Name = s.Location.Name,
                SpaceType = s.Location.SpaceType,
                Description = s.Location.Description
            };
        }

        if (s.Client != null)
        {
            resp.Client = new UserResponse
            {
                Name = s.Client.Name,
                Email = s.Client.Email
            };
        }

        if (s.Photographer != null)
        {
            resp.Photographer = new PhotographerResponse
            {
                Id = s.Photographer.Id,
                Name = s.Photographer.Name,
                Email = s.Photographer.Email,
                Phone = s.Photographer.Phone,
                Role = s.Photographer.Rol
            };
        }

        return resp;
    }

    public PhotoSessionResponse DeletePhotoSession(int id)
    {
        var session = _repo.GetById(id);
        if (session == null)
        {
            return null;
        }
        session.Status = SessionStatus.Cancelled;
        var ok = _repo.Update(session);
        return ok ? Map(_repo.GetById(id)) : null;  
    }
}