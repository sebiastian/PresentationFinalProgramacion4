using Application.Interfaces;
using Application.Abstraction;
using Contract.Authentication.Request;
using Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System;

namespace Application.Service;

public class AuthenticationService : IAuthenticationService
{
    private readonly IPhotographerRepository _photographerRepo;
    private readonly IConfiguration _config;

    public AuthenticationService(IPhotographerRepository photographerRepo, IConfiguration config)
    {
        _photographerRepo = photographerRepo;
        _config = config;
    }

    public string Authenticate(AuthenticationRequest request)
    {
        // Hashea la contraseña antes de comparar
        var hashedPassword = HashPassword(request.Password);
        
        // Busca el fotógrafo por email y password hasheado
        var photographer = _photographerRepo.GetAll()
            .FirstOrDefault(p => p.Email == request.Email && p.PasswordHash == hashedPassword);

        if (photographer == null)
            return null;

        // Crea los claims para el JWT
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, photographer.Email),
            new Claim(ClaimTypes.NameIdentifier, photographer.Id.ToString()),
            new Claim(ClaimTypes.Role, photographer.Rol)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string HashPassword(string password)
    {
        // Mismo algoritmo que en PhotographerService
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashed = sha.ComputeHash(bytes);
        return Convert.ToHexString(hashed);
    }
}