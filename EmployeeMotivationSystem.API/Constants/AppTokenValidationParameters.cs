using Microsoft.IdentityModel.Tokens;

namespace EmployeeMotivationSystem.API.Constants;

public sealed class AppTokenValidationParameters : TokenValidationParameters
{
    public AppTokenValidationParameters()
    {
        // указывает, будет ли валидироваться издатель при валидации токена
        ValidateIssuer = true;
        
        // строка, представляющая издателя
        ValidIssuer = AppAuthOptions.Issuer;
        
        // будет ли валидироваться потребитель токена
        ValidateAudience = true;
        
        // установка потребителя токена
        ValidAudience = AppAuthOptions.Audience;
        
        // будет ли валидироваться время существования
        ValidateLifetime = true;
        
        // установка ключа безопасности
        IssuerSigningKey = AppAuthOptions.SymmetricSecurityKey;
        
        // валидация ключа безопасности
        ValidateIssuerSigningKey = true;
    }
}