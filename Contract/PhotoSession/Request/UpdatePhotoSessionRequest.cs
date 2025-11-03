using Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Contract.PhotoSession.Request;

public class UpdatePhotoSessionRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public SessionType SessionType { get; set; }
    
    [Required]
    public SessionStatus Status { get; set; }
    
    [Required]
    public int PhotoCount { get; set; }
    
    [Required]
    [StringLength(50)]
    public string DeliveryFormat { get; set; }
    
    [Required]
    public int LocationId { get; set; }
    
    [Required]
    public int ClientId { get; set; }
    
    public int? PhotographerId { get; set; }
}
