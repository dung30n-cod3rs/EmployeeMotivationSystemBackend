using System.IdentityModel.Tokens.Jwt;
using EmployeeMotivationSystem.API.Constants;
using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.API.Middleware.RoleCheck;

public sealed class RoleCheckMiddleware : ActionFilterAttribute
{
    public override async void OnActionExecuting(ActionExecutingContext context)
    {
        string? authToken = context.HttpContext.Request.Headers.Authorization;
        
        authToken = authToken?.Replace("Bearer ", string.Empty);
        
        if (string.IsNullOrEmpty(authToken)) 
            return;
        
        var handler = new JwtSecurityTokenHandler();
        var validations = new AppTokenValidationParameters();
        
        handler.ValidateToken(authToken, validations, out var outSecurityToken);
        
        var jwtToken = outSecurityToken as JwtSecurityToken;
        
        var userId = jwtToken?.Claims.SingleOrDefault(el => el.Type == AppCustomClaimTypes.UserId)?.Value;
        
        if (userId == null)
            throw new Exception("RoleMethod wants to check UserId but it null.");
        
        var dbContext = context.HttpContext
            .RequestServices
            .GetService(typeof(AppDbContext)) as AppDbContext;

        if (dbContext == null)
            throw new Exception("DbContext is null, wtf, fix that.");

        var company = await dbContext.Companies
            .SingleOrDefaultAsync(el => el.CreatorUserId.ToString() == userId);

        if (company == null)
        {
            context.Result = new ObjectResult("Access denied.")
            {
                StatusCode = 401
            };
            
            return;
        }
    }
}