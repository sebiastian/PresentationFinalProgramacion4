using Contract.Location.Response;
using Contract.Photographer.Response;
using Contract.User.Response;
using System;

namespace Contract.PhotoSession.Response;

public class PhotoSessionResponse
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string SessionType { get; set; }
    public string Status { get; set; }
    public int PhotoCount { get; set; }
    public string DeliveryFormat { get; set; }
    public int LocationId { get; set; }
    public int ClientId { get; set; }
    public int? PhotographerId { get; set; }

    public LocationResponse Location { get; set; }
    public UserResponse Client { get; set; }
    public PhotographerResponse Photographer { get; set; }
}