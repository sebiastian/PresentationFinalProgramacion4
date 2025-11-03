using Application.Abstraction;
using Domain.Entity;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Infrastructure.Persistence.Repository;

public class PhotoSessionRepository : IPhotoSessionRepository
{
    private static readonly Dictionary<int, PhotoSession> _sessions;
    private readonly ILocationRepository _locationRepo;
    private readonly IUserRepository _userRepo;
    private readonly IPhotographerRepository _photographerRepo;
    private static readonly object _lock = new();
    private static bool _initialized = false;

    static PhotoSessionRepository()
    {
        _sessions = new Dictionary<int, PhotoSession>();
    }

    public PhotoSessionRepository(
        ILocationRepository locationRepo,
        IUserRepository userRepo,
        IPhotographerRepository photographerRepo)
    {
        _locationRepo = locationRepo;
        _userRepo = userRepo;
        _photographerRepo = photographerRepo;

        // Inicializar datos de prueba solo una vez
        lock (_lock)
        {
            if (!_initialized)
            {
                _sessions.Add(1, new PhotoSession
                {
                    Id = 1,
                    Date = DateTime.UtcNow.AddDays(-2),
                    SessionType = SessionType.Portrait,
                    Status = SessionStatus.Finished,
                    PhotoCount = 50,
                    DeliveryFormat = "PNG",
                    LocationId = 1,
                    ClientId = 1,
                    PhotographerId = 1
                });

                _sessions.Add(2, new PhotoSession
                {
                    Id = 2,
                    Date = DateTime.UtcNow,
                    SessionType = SessionType.Wedding,
                    Status = SessionStatus.Confirmed,
                    PhotoCount = 200,
                    DeliveryFormat = "JPG",
                    LocationId = 2,
                    ClientId = 2,
                    PhotographerId = 2
                });

                _sessions.Add(3, new PhotoSession
                {
                    Id = 3,
                    Date = DateTime.UtcNow.AddDays(5),
                    SessionType = SessionType.Family,
                    Status = SessionStatus.Pending,
                    PhotoCount = 30,
                    DeliveryFormat = "JPG",
                    LocationId = 3,
                    ClientId = 3,
                    PhotographerId = null
                });

                foreach (var session in _sessions.Values)
                {
                    PopulateNavigationProperties(session);
                }

                _initialized = true;
            }
        }
    }

    private void PopulateNavigationProperties(PhotoSession session)
    {
        session.Location = _locationRepo.GetLocation(session.LocationId.ToString());
        session.Client = _userRepo.GetUserById(session.ClientId);
        session.Photographer = session.PhotographerId.HasValue
            ? _photographerRepo.GetPhotographerById(session.PhotographerId.Value)
            : null;
    }

    public PhotoSession GetById(int id)
    {
        lock (_lock)
        {
            if (_sessions.TryGetValue(id, out var session))
            {
                return session;
            }
            return null;
        }
    }

    public List<PhotoSession> GetAll()
    {
        lock (_lock)
        {
            return _sessions.Values.ToList();
        }
    }

    public PhotoSession Create(PhotoSession newSession)
    {
        lock (_lock)
        {
            int newId = _sessions.Any() ? _sessions.Keys.Max() + 1 : 1;
            newSession.Id = newId;

            // Validar/normalizar campos mínimos si es necesario (no nulos)
            PopulateNavigationProperties(newSession);

            // Usar indexador para sobrescribir si ya existiera la clave por alguna razón
            _sessions[newId] = newSession;

            return newSession;
        }
    }

    public bool Update(PhotoSession session)
    {
        if (session == null) return false;

        lock (_lock)
        {
            if (!_sessions.ContainsKey(session.Id))
            {
                return false;
            }

            // Asegurar que las relaciones estén pobladas antes de guardar
            PopulateNavigationProperties(session);

            _sessions[session.Id] = session;
            return true;
        }
    }

    public PhotoSession Delete(int id)
    {
        lock (_lock)
        {
            if (_sessions.TryGetValue(id, out var session))
            {
                _sessions.Remove(id);
                return session;
            }
            return null;
        }   
    }
}