using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EmployeeMotivationSystem.API.Constants;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeMotivationSystem.API.Middleware.Jwt;

public sealed class JwtMiddleware : ActionFilterAttribute
{
    public const string JwtTokenHttpContextKey = "JwtToken";
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        string? authToken = context.HttpContext.Request.Headers.Authorization;
        
        authToken = authToken?.Replace("Bearer ", string.Empty);
        
        if (string.IsNullOrEmpty(authToken)) 
            return;
        
        var handler = new JwtSecurityTokenHandler();
        var validations = new AppTokenValidationParameters();

        SecurityToken? validatedToken = null;
        
        try
        {
            handler.ValidateToken(authToken, validations, out var outSecurityToken);
            validatedToken = outSecurityToken;
        }
        catch (Exception)
        {
            // ignored
        }
        
        var jwtToken = validatedToken as JwtSecurityToken;
        
        var email = jwtToken?.Claims.SingleOrDefault(el => el.Type == ClaimTypes.Email)?.Value;

        context.HttpContext.Items[JwtTokenHttpContextKey] = email;
    }
    
}