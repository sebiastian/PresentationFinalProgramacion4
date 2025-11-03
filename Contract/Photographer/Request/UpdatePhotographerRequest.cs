using System.ComponentModel.DataAnnotations;

namespace Contract.Photographer.Request
{
    public class UpdatePhotographerRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = null!;

        [StringLength(20)]
        public string? Phone { get; set; }

        // Password opcional en actualización
        [StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = null!;
    }
}