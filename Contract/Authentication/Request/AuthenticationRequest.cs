using System.ComponentModel.DataAnnotations;

namespace Contract.Authentication.Request
{
    public class AuthenticationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = null!;
    }
}