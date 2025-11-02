using System;

namespace Domain.Entity;

public enum SessionType
{
    Portrait,
    Wedding,
    Product,
    Family,
    Other
}

public enum SessionStatus
{
    Pending,
    Confirmed,
    Finished,
    Cancelled
}

public class PhotoSession
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public SessionType SessionType { get; set; }
    public SessionStatus Status { get; set; }
    public int PhotoCount { get; set; }
    public string DeliveryFormat { get; set; }

    // FKs & navigation
    public int LocationId { get; set; }
    public Location Location { get; set; }

    public int ClientId { get; set; }
    public User Client { get; set; }

    public int? PhotographerId { get; set; }
    public Photographer Photographer { get; set; }
}