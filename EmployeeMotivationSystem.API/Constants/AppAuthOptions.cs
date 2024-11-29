using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeMotivationSystem.API.Constants;

public class AppAuthOptions
{
    private const string AuthKey = "M*F8s*sdf7929rh9Hf8S8f79s9__euf";
    
    public const string Issuer = "EmployeeMotivationSystemBackend";
    public const string Audience = "EmployeeMotivationSystemFrontend";
    
    public static SymmetricSecurityKey SymmetricSecurityKey => new(Encoding.UTF8.GetBytes(AuthKey));
}