using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeMotivationSystem.API.Constants;

public static class AppAuthOptions
{
    private const string AuthKey = "supersecretkey_supersecretkey_supersecretkey_supersecretkey"; // TODO: To app config
    
    public const string Issuer = "EmployeeMotivationSystemBackend"; // TODO: To app config
    public const string Audience = "EmployeeMotivationSystemFrontend"; // TODO: To app config
    
    public static SymmetricSecurityKey SymmetricSecurityKey => new(Encoding.UTF8.GetBytes(AuthKey));

    public static string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        var jwt = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
            signingCredentials: new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));
            
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}