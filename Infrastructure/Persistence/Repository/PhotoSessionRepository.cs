using Domain.Abstraction;
using Domain.Entity;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository;

public class PhotoSessionRepository : RepositoryBase<PhotoSession>, IPhotoSessionRepository
{
    public PhotoSessionRepository(UserManagerDbContext context) : base(context)
    {
    }

    public new PhotoSession GetById(int id)
    {
        return _context.PhotoSessions
            .Include(ps => ps.Location)
            .Include(ps => ps.Client)
            .Include(ps => ps.Photographer)
            .FirstOrDefault(ps => ps.Id == id);
    }

    public new List<PhotoSession> GetAll()
    {
        return _context.PhotoSessions
            .Include(ps => ps.Location)
            .Include(ps => ps.Client)
            .Include(ps => ps.Photographer)
            .ToList();
    }

    public new PhotoSession Create(PhotoSession newSession)
    {
        _context.PhotoSessions.Add(newSession);
        _context.SaveChanges();
        
        return GetById(newSession.Id);
    }

    public new bool Update(PhotoSession session)
    {
        var existing = _context.PhotoSessions.FirstOrDefault(ps => ps.Id == session.Id);
        if (existing == null) return false;

        existing.Date = session.Date;
        existing.SessionType = session.SessionType;
        existing.Status = session.Status;
        existing.PhotoCount = session.PhotoCount;
        existing.DeliveryFormat = session.DeliveryFormat;
        existing.LocationId = session.LocationId;
        existing.ClientId = session.ClientId;
        existing.PhotographerId = session.PhotographerId;

        _context.SaveChanges();
        return true;
    }

    public new PhotoSession Delete(int id)
    {
        var session = _context.PhotoSessions.FirstOrDefault(ps => ps.Id == id);
        if (session == null) return null;

        _context.PhotoSessions.Remove(session);
        _context.SaveChanges();
        return session;
    }
}