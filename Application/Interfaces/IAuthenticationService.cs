using Contract.Authentication.Request;

namespace Application.Interfaces;

public interface IAuthenticationService
{
    /// <summary>
    /// Autentica un usuario y devuelve un JWT si es válido, null si no.
    /// </summary>
    /// <param name="request">DTO con email y password</param>
    /// <returns>Token JWT o null</returns>
    string Authenticate(AuthenticationRequest request);
}   