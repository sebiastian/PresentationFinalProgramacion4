using Application.Interfaces;
using Contract.Authentication.Request;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = _authService.Authenticate(request);

            if (token == null)
                return Unauthorized("Credenciales inválidas.");

            return Ok(new { token });
        }
    }
}