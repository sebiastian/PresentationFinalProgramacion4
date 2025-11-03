using Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Contract.PhotoSession.Request;

public class CreatePhotoSessionRequest
{
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public SessionType SessionType { get; set; }
    
    [Required]
    public int LocationId { get; set; }
    
    [Required]
    public int ClientId { get; set; }
    
    public int? PhotographerId { get; set; }
    
    [Required]
    [StringLength(50)]
    public string DeliveryFormat { get; set; }
}