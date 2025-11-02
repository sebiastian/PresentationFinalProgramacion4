using Application.Abstraction;
using Domain.Entity;
using System;

namespace Infrastructure.Persistence.Repository;

public class PhotoSessionRepository : IPhotoSessionRepository
{
    public PhotoSession GetById(int id)
    {
        // Ejemplo simple
        return new PhotoSession
        {
            Id = id,
            Date = DateTime.UtcNow,
            SessionType = SessionType.Portrait,
            Status = SessionStatus.Pending,
            PhotoCount = 20,
            DeliveryFormat = "JPG",
            LocationId = 1,
            ClientId = 2,
            PhotographerId = 1
        };
    }
}