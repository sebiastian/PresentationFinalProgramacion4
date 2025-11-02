using System.ComponentModel.DataAnnotations;

namespace Contract.User.Request
{
    public class CreateUserRequest
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
    }
}
