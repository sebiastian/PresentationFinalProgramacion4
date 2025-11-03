using System.ComponentModel.DataAnnotations;

namespace Contract.Photographer.Request
{
    public class CreatePhotographerRequest
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

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = null!;
    }
}